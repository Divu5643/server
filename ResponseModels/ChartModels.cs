namespace perfomanceSystemServer.ResponseModels
{
    public class EmployeePieChart
    {
        public string? employeeRole { get; set; }
        public int totalEmployee {  get; set; }
    }
    public class BarChartResponse
    {
        public int year { get; set; }
        public int month { get; set; }
        public int completedGoal { get; set; }
        public int pendingGoal { get; set; }
        public int progressGoal { get; set; }

    }
    public class PerfomanceBarChartResponse {
        public int year { get; set; }
        public int month { get; set; }
        public int averageScore { get; set; }
    }
}
