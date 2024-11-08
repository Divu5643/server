using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Services
{
    public class PerformanceService : IPerformance
    {
        private readonly Iuser _user;
        private readonly AppDbContext _appDbContext;
        public PerformanceService(AppDbContext appDbContext, Iuser user)
        {

            _appDbContext = appDbContext;
            _user = user;
        }
        public Performance? savePerformance(AddPerformance review) {
            List<int> adminIdList = new List<int>();
            foreach (var item in _user.getAdmins())
            {
                adminIdList.Add(item.Userid);
            }
            Boolean isEmployee = (_appDbContext.Users
                .Where(user => user.Userid == review.userId && user.RoleId == 3))
                .Select(user => true).FirstOrDefault();
            Boolean isManager = (_appDbContext.Reviewers
               .Where(user => user.EmployeeId == review.userId && 
               (user.ManagerId == review.createdBy|| adminIdList.Contains(review.createdBy) ) ))
               .Select(user => true).FirstOrDefault();
            if (review.userId == review.createdBy) { isManager = true; }

            if (!isEmployee || !isManager) { return null; }
            Performance newReview = new Performance()
            {
                UserId = review.userId,
                TechnicalSkill = review.technicalSkill,
                SoftSkill = review.softSkill,
                Teamwork = review.teamworkSkill,
                DeliveryTime = review.deliveryTime,
                Remark = review.remark,
                CreatedBy = review.createdBy,
            };
            _appDbContext.Performances.Add(newReview);
            _appDbContext.SaveChanges();
            return newReview; 
        }
        public double getAverageScore()
        {
            try
            {
                double score = _appDbContext.Performances.Select(
                    score => score.TechnicalSkill + score.SoftSkill + score.Teamwork + score.DeliveryTime).Average();
                return score;
            }catch(Exception e)
            {
                Console.WriteLine(e);
                return 0.00;
            }
        }

        public double getAverageScoreByManager(int managerId)
        {
            try
            {
                double score = (from marks in _appDbContext.Performances
                                join manager in _appDbContext.Reviewers on marks.UserId equals manager.EmployeeId
                                where manager.ManagerId == managerId && !manager.IsDeleted
                                let totalMarks = (marks.TechnicalSkill + 
                                marks.SoftSkill + marks.Teamwork + marks.DeliveryTime)
                                select totalMarks
                                ).Average();

                return score;
            }catch(Exception e)
            {
                Console.WriteLine(e);

                return 0.0;
            }
        }
        public List<Performance> GetPerformance(int employeeId)
        {
            List<Performance> performanceList = _appDbContext.Performances
                                            .Where(obj => obj.UserId == employeeId && !obj.IsDeleted)
                                            .OrderByDescending(obj => obj.CreatedDate).ToList();

            return performanceList;
        }
        public Performance? markAsDeleted(int performanceId) {
            Performance deletedPerformace = _appDbContext.Performances.Find(performanceId)!;
            if (deletedPerformace == null) { return null; }
            deletedPerformace.IsDeleted = true;
            _appDbContext.SaveChanges();
            return deletedPerformace;
        
        }

        public List<PerfomanceBarChartResponse> getBarChartData() {
            List<PerfomanceBarChartResponse> goalData = (from ym in _appDbContext!.YearAndMonths
                                               join performance in _appDbContext.Performances on 
                                               new { year = ym.FetchYear!.Value.Year, month = ym.FetchMonth!.Value.Month }
                                               equals 
                                               new { year = performance.CreatedDate.Year, month = performance.CreatedDate.Month }
                                               into perfomanceGroup
                                               from performance in perfomanceGroup.DefaultIfEmpty()
                                               where performance != null || !performance.IsDeleted
                                               group performance by new
                                               {
                                                   ym.FetchYear!.Value.Year,
                                                   ym.FetchMonth!.Value.Month
                                               } into grouped

                                               select new PerfomanceBarChartResponse
                                               {
                                                   month = grouped.Key.Month,
                                                   averageScore =  Convert.ToInt32(grouped.Where(g=> g != null)
                                                   .Select(obj => obj.DeliveryTime + obj.Teamwork + obj.SoftSkill +
                                                   obj.TechnicalSkill).Average()),
                                                   year = grouped.Key.Year,
                                               }).ToList();
            return goalData;

        }

    }
}
