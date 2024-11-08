namespace perfomanceSystemServer.ResponseModels
{
    public class EmployeeProfile
    {
        public int userId { get; set; }
        public string? name { get; set; }
        public string? designation { get; set; }
        public string? department { get; set; }
        public string? reportingManager { get; set; }
        public DateOnly? dateOfBirth { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? personalEmail { get; set; }
        public string? role { get; set; }
    }
}
