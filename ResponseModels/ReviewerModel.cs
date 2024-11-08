namespace perfomanceSystemServer.ResponseModels
{
    public class ReviewerModel
    {
        public int employeeId { get; set; }
        public int managerId { get; set; }
        public string? reviewType { get; set; }
    }
    public class EmployeeAndManager
    {
        public int reviewId { get; set; }
        public int employeeId { get; set; }
        public string? employeeName { get; set; }
        public int managerId { get; set; }
        public string? managerName { get; set; }
        public string? reviewType { get; set; }
    }
    public class paginationEmployeeAndManager {
     
        public List<EmployeeAndManager>? ReviewList { get; set; }
        public int TotalCount { get; set; }

    }

    public class IreviewId
    {
        public int reviewId { get; set; }
    }

    public class IuserByManager{

        public int managerId { get; set; }
        public int employeeId { get; set; }
        public string? employeeName { get; set; }
        public string? email { get; set; }
        public string? department { get; set; }

        }
    
}
