namespace perfomanceSystemServer.ResponseModels
{
    public class ShowGoals
    {
        public int? goalId { get; set; }
        public string? employeeName { get; set; }
        public int? employeeId { get; set; }
        public string? goalOutcome { get; set; }

        public DateOnly completionDate { get; set; }
        public DateOnly startDate { get; set; }
        public string? assignerName { get; set; }
        public int assignerId { get; set; }
        public string? status { get; set; }
    }
    public class NewGoal
    {
        public int userId { get; set; }
        public string? goalOutcome { get; set; }
        public DateOnly completionDate { get; set; }
        public int createdBy { get; set; }
    }
    public class EditGoal : NewGoal
    {
       
        public string? status { get; set; }
        
    }

    public class goalPaginationResponse{
        public List<ShowGoals>? Goals { get; set; }
        public int total { get; set; }
    }
}
