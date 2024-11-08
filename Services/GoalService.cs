using Microsoft.AspNetCore.Http.HttpResults;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;
using System;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
namespace perfomanceSystemServer.Services
{
    public class GoalService : Igoal
    {
        private readonly AppDbContext? _appDbContext;
        public GoalService(AppDbContext? appDbContext)
        {

            _appDbContext = appDbContext;
        }
        public Goal AddGoal(NewGoal newGoal)
        {
            Goal tempGoal = new Goal();
            tempGoal.UserId = newGoal.userId;
            tempGoal.CompletionDate = newGoal.completionDate;
            tempGoal.GoalOutcome = newGoal.goalOutcome!;
            tempGoal.CreatedBy = newGoal.createdBy;
            tempGoal.Status = "pending";

            _appDbContext!.Add(tempGoal);
            _appDbContext.SaveChanges();
            return tempGoal;
        }

        public Goal? DeleteGoal(int goalId)
        {
            Goal? deletedGoal  = _appDbContext!.Goals.Find(goalId);
            if (deletedGoal == null) { return deletedGoal; }
            deletedGoal.IsDeleted= true;
            _appDbContext.Update(deletedGoal);
            _appDbContext.SaveChanges();
            return deletedGoal;
        }

        public List<ShowGoals> getAllGoals()
        {
            List<ShowGoals> showGoals = (from goal in _appDbContext!.Goals
                                        join emp in _appDbContext.Users on goal.UserId equals emp.Userid
                                        join assigner in _appDbContext.Users on goal.CreatedBy equals assigner.Userid
                                        where !goal.IsDeleted && !emp.IsDeleted
                                        orderby goal.GoalId descending
                                         select  new ShowGoals {
                                           goalId= goal.GoalId,
                                           employeeId = emp.Userid,
                                           employeeName = emp!.Name,
                                           assignerId = assigner.Userid,
                                           assignerName = assigner!.Name,
                                           goalOutcome = goal.GoalOutcome,
                                           completionDate = goal!.CompletionDate,
                                           status = goal.Status
                                        }).ToList();
            return showGoals;
        }
        public Goal? editGoal(EditGoal changedGoal)
        {
            Goal toBeUpdatedGoal = _appDbContext!.Goals.Find(changedGoal.userId)!;
            if (toBeUpdatedGoal.Status == "complete") { return null; }
            if (toBeUpdatedGoal.Status == "in-progress" && changedGoal.status == "pending") {  return null; }
            toBeUpdatedGoal.Status = changedGoal.status;
            toBeUpdatedGoal.GoalOutcome = changedGoal.goalOutcome!;
            toBeUpdatedGoal.CompletionDate = changedGoal.completionDate;

            _appDbContext.Goals.Update(toBeUpdatedGoal);
            _appDbContext.SaveChanges();
            return toBeUpdatedGoal;   
        }
        public List<ShowGoals> goalsForEmloyee(int userId)
        {
            List<ShowGoals> employeeGoals = (from goal in _appDbContext!.Goals
                                            join emp in _appDbContext.Users on goal.UserId equals emp.Userid
                                            join assigner in _appDbContext.Users on goal.CreatedBy equals assigner.Userid
                                            where !goal.IsDeleted  && goal.UserId == userId
                                            orderby goal.GoalId descending
                                            select new ShowGoals
                                            {
                                                goalId = goal!.GoalId ,
                                                employeeId = emp!.Userid,
                                                employeeName = emp.Name,
                                                assignerId = assigner.Userid,
                                                assignerName = assigner!.Name,
                                                goalOutcome = goal!.GoalOutcome,
                                                completionDate = goal.CompletionDate,
                                                status = goal.Status,
                                                startDate =  goal.CreatedDate
                                            }).ToList();
            return employeeGoals;

        }
        public Goal? markAsInProgress(int goalId)
        {
            Goal? editedGoal = _appDbContext!.Goals.Find(goalId);
            if (editedGoal == null) { return editedGoal; }
            editedGoal.Status = "in-progress";
            _appDbContext.Update(editedGoal);
            _appDbContext.SaveChanges();
            return editedGoal;
        }
        public List<ShowGoals> goalsByManagerID(int managerId)
        {
            List<ShowGoals> employeeGoals = (from goal in _appDbContext!.Goals
                                             join manager in _appDbContext.Users on goal.CreatedBy equals manager.Userid
                                             join review in _appDbContext.Reviewers on goal.UserId equals review.EmployeeId
                                             join emp in _appDbContext.Users on goal.UserId equals emp.Userid
                                             where review.ManagerId == managerId && !goal.IsDeleted && !review.IsDeleted
                                             orderby goal.GoalId descending
                                             select new ShowGoals
                                             {
                                                 employeeName = emp!.Name,
                                                 assignerId = goal!.CreatedBy,
                                                 goalId = goal.GoalId,
                                                 employeeId = goal.UserId,
                                                 goalOutcome = goal.GoalOutcome,
                                                 completionDate = goal.CompletionDate,
                                                 assignerName = manager!.Name,
                                                 status = goal.Status
                                             }).ToList();
            return employeeGoals;
        }

        public List<BarChartResponse> getGoalDataForChart()
        {
            List<BarChartResponse> goalData = (from ym in _appDbContext!.YearAndMonths
                                               join goal in _appDbContext.Goals on new { year = ym.FetchYear!.Value.Year, month = ym.FetchMonth!.Value.Month }
                                               equals new { year = goal.CompletionDate.Year, month = goal.CompletionDate.Month }
                                               into goalGroup
                                               from goal in goalGroup.DefaultIfEmpty()
                                               where goal ==null|| !goal.IsDeleted 
                                               group goal by new { ym.FetchYear!.Value.Year, 
                                                   ym.FetchMonth!.Value.Month
                                          } into grouped
                                                                                         
                                               select new BarChartResponse
                                               {
                                                   month = grouped.Key.Month,
                                                   completedGoal = grouped.Count(g=> g!=null && g.Status =="complete"),
                                                   progressGoal = grouped.Count(g => g != null && g.Status == "in-progress"),
                                                   pendingGoal = grouped.Count(g => g != null && g.Status == "pending"),
                                                   year = grouped.Key.Year,
                                               }).ToList();
            return goalData;
        }

        public List<DisplayComment> getCommentsForGoal(int goalId)
        {
            List<DisplayComment> CommentList =  (from comment in _appDbContext!.Comments
                                                join user in _appDbContext.Users on comment.CreatedBy equals user.Userid
                                                where comment.GoalId == goalId
                                                orderby comment.CreatedDate descending
                                                select new DisplayComment
                                                {
                                                    CommentId = comment.CommentId,
                                                    GoalId =comment.GoalId,
                                                    CommentText = comment.CommentText,
                                                    CreatedBy = comment.CreatedBy,
                                                    CreatedDate = comment.CreatedDate,
                                                    CreatedName = user.Name

                                                }).ToList();
            return CommentList;
        }

        public Comment? addComment(CommentResponse newComment)
        {
            Comment comment1 =  new Comment() {
                GoalId = newComment.GoalId,
                CreatedBy =  newComment.CreatedBy,
                CommentText = newComment.CommentText
            };
            _appDbContext!.Comments.Add(comment1);
            _appDbContext.SaveChanges();
            return comment1;
        }

        public ShowGoals? getSpecificGoal(int goalId)
        {
            ShowGoals? RetrievedGoal = (from goal in _appDbContext!.Goals
                                      join manager in _appDbContext.Users on goal.CreatedBy equals manager.Userid
                                      join emp in _appDbContext.Users on goal.UserId equals emp.Userid
                                      where goal.GoalId == goalId && !goal.IsDeleted
                                      select new ShowGoals
                                      {
                                          employeeName = emp!.Name,
                                          assignerId = goal!.CreatedBy,
                                          goalId = goal.GoalId,
                                          employeeId = goal.UserId,
                                          goalOutcome = goal.GoalOutcome,
                                          completionDate = goal.CompletionDate,
                                          assignerName = manager!.Name,
                                          status = goal.Status
                                      }).FirstOrDefault();


            return RetrievedGoal;

        }

        public goalPaginationResponse getGoalsForSearchAndPagination(GoalPagination pageNum)
        {
            IQueryable<ShowGoals> query;
            if (pageNum.managerId == 0)
            {
                query = AllGoals();
            }
            else {
                query = goalForManager(pageNum.managerId);
            }
            int skipRows = pageNum.PageNumber * pageNum.RowCount;
           
            List<ShowGoals> showGoals =  new List<ShowGoals>();
            if (pageNum.Search =="")
            {
                if (pageNum.FilterValue == "all")
                {
                   
                }
                else
                {
                    query = query.Where(obj => obj.status == pageNum.FilterValue);
                        
                }
            }
            else
            {
                if (pageNum.FilterValue == "all")
                {

                    query = query.Where(obj => obj.goalOutcome!.Contains(pageNum.Search!.ToLower()));
                        
                }
                else
                {
                    query = query.Where(obj => obj.status == pageNum.FilterValue
                    && obj.goalOutcome!.Contains(pageNum.Search!.ToLower()));
                }
            }
            int totalCount = query.ToList().Count();
            showGoals =  query.Skip(skipRows).Take(pageNum.RowCount).ToList();
            return new goalPaginationResponse()
            {
                Goals = showGoals,
                total = totalCount
            };
        }

        public IQueryable<ShowGoals> goalForManager(int? managerId)
        {
            IQueryable<ShowGoals> employeeGoals = (from goal in _appDbContext!.Goals
                                             join manager in _appDbContext.Users on goal.CreatedBy equals manager.Userid
                                             join review in _appDbContext.Reviewers on goal.UserId equals review.EmployeeId
                                             join emp in _appDbContext.Users on goal.UserId equals emp.Userid
                                             where review.ManagerId == managerId && !goal.IsDeleted && !review.IsDeleted
                                             orderby goal.GoalId descending
                                             select new ShowGoals
                                             {
                                                 employeeName = emp!.Name,
                                                 assignerId = goal!.CreatedBy,
                                                 goalId = goal.GoalId,
                                                 employeeId = goal.UserId,
                                                 goalOutcome = goal.GoalOutcome,
                                                 completionDate = goal.CompletionDate,
                                                 assignerName = manager!.Name,
                                                 status = goal.Status
                                             }).AsQueryable();
            return employeeGoals;
        }

        public IQueryable<ShowGoals> AllGoals() {

            IQueryable<ShowGoals> query = (from goal in _appDbContext!.Goals
                                           join emp in _appDbContext.Users on goal.UserId equals emp.Userid
                                           join assigner in _appDbContext.Users on goal.CreatedBy equals assigner.Userid
                                           where !goal.IsDeleted && !emp.IsDeleted
                                           orderby goal.GoalId descending
                                           select new ShowGoals
                                           {
                                               goalId = goal.GoalId,
                                               employeeId = emp.Userid,
                                               employeeName = emp!.Name,
                                               assignerId = assigner.Userid,
                                               assignerName = assigner!.Name,
                                               goalOutcome = goal.GoalOutcome,
                                               completionDate = goal!.CompletionDate,
                                               status = goal.Status
                                           }).AsQueryable();
            return query;
        }

    }
}
