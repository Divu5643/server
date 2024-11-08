namespace perfomanceSystemServer.ResponseModels
{
    public class CommentResponse
    {
        public int GoalId { get; set; }
        public string? CommentText { get; set; }
        public int CreatedBy { get; set; }

    }
    public class DisplayComment
    {
        public int CommentId { get; set; }
        public int? GoalId { get; set; }
        public string? CommentText { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
