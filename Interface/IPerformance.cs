using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Interface
{
    public interface IPerformance
    {
        public Performance? savePerformance(AddPerformance review);
        public List<Performance> GetPerformance(int employeeId);
        public double getAverageScore();
        public double getAverageScoreByManager(int managerId);
        public Performance? markAsDeleted(int performanceId);
        public List<PerfomanceBarChartResponse> getBarChartData();
    }
}
