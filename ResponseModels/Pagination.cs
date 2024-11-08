namespace perfomanceSystemServer.ResponseModels
{
    public class Pagination
    {
     
        public int RowCount {  get; set; }
        public int PageNumber { get; set; }
        public int RoleId { get; set; }
        public string? Search { get; set; }
    }     

    public class GoalPagination
    {
        public int RowCount { get; set; }
        public int PageNumber { get; set; }
        public string? FilterValue{ get; set; }
        public string? Search { get; set; }
        public int? managerId { get; set; } = 0;
    }
    public class ReviewPagination
    {
        public int RowCount { get; set; }
        public int PageNumber { get; set; }

    }
    
}
