using Microsoft.AspNetCore.Mvc.ModelBinding;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Services
{
    public class ReviewerService : Ireviewer
    {

        private readonly AppDbContext _appDbContext;
        public ReviewerService(AppDbContext appDbContext) {

            _appDbContext = appDbContext;
        }
        public Reviewer? AddReviewer(ReviewerModel review)
        {
            try
            {
                Reviewer tempReview = new Reviewer()
                {
                    EmployeeId = review.employeeId,
                    ManagerId = review.managerId,
                    ReviewType = review.reviewType,
                };
                _appDbContext.Reviewers.Add(tempReview);
            _appDbContext.SaveChanges();
                return tempReview;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public IQueryable<EmployeeAndManager> getAllReviews() {
            IQueryable<EmployeeAndManager> reviewerList = (from review in _appDbContext.Reviewers
                                                     join emp in _appDbContext.Users on review.EmployeeId equals emp.Userid
                                                     join manager in _appDbContext.Users on review.ManagerId equals manager.Userid
                                                     where !review.IsDeleted
                                                     select new EmployeeAndManager
                                                     {
                                                         reviewId = review!.ReviewerId,
                                                         employeeId = emp!.Userid,
                                                         managerId = manager!.Userid,
                                                         employeeName = emp.Name,
                                                         managerName = manager.Name,
                                                         reviewType = review.ReviewType
                                                     }).AsQueryable();
            return reviewerList;
        }
        public paginationEmployeeAndManager getReviewsWithPagination(ReviewPagination paginationValues) {
        List<EmployeeAndManager> reviewList  = new List<EmployeeAndManager>();
            IQueryable<EmployeeAndManager> reviewerList = getAllReviews();
            int skipRows = paginationValues.RowCount * paginationValues.PageNumber;
           
            int total = reviewerList.Count();
            reviewList = reviewerList.Skip(skipRows).Take(paginationValues.RowCount).ToList();

            return new paginationEmployeeAndManager() {
            ReviewList =  reviewList,
            TotalCount=total
            };
        }
        public Reviewer? DeleteReviewer(int reviewID)
        {
            try
            {
                Reviewer? deletedReviewer = _appDbContext.Reviewers.Find(reviewID);
                if (deletedReviewer == null) { return null; }
                deletedReviewer.IsDeleted = true;
                _appDbContext.SaveChanges();
                return deletedReviewer;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<IuserByManager> getUserByManager(int managerId)
        {
            List<IuserByManager> userManager = (from review in _appDbContext.Reviewers
                                                join emp in _appDbContext.Users on review.EmployeeId equals emp.Userid
                                                join manager in _appDbContext.Users on review.ManagerId equals manager.Userid
                                                join dept in _appDbContext.DepartmentMasters on emp.DeptId equals dept.DeptId
                                                where manager.Userid == managerId && !review.IsDeleted  
                                                select new IuserByManager
                                                {
                                                    managerId = review!.ManagerId,
                                                    employeeId = review.EmployeeId,
                                                    employeeName = emp!.Name,
                                                    email = emp.Email,
                                                    department = dept.Department,
                                                }).ToList();
            return userManager;
        }

        public List<EmployeeAndManager> getReviewerHistory(int userId)
        {
            List<EmployeeAndManager> reviewerList = (from review in _appDbContext.Reviewers
                                                     join emp in _appDbContext.Users on review.EmployeeId equals emp.Userid
                                                     join manager in _appDbContext.Users on review.ManagerId equals manager.Userid
                                                     where review.EmployeeId==userId && review.IsDeleted
                                                     select new EmployeeAndManager
                                                     {
                                                         reviewId = review!.ReviewerId,
                                                         employeeId = emp!.Userid,
                                                         managerId = manager!.Userid,
                                                         employeeName = emp.Name,
                                                         managerName = manager.Name,
                                                         reviewType = review.ReviewType
                                                     }).ToList();

            return reviewerList;
        }

    }
}
