namespace perfomanceSystemServer.ResponseModels
{
    public class UserModel
    {
       public int userid { get; set; }
        public string? name{ get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public int roleId { get; set; }
        public int deptId { get; set; }
        public int designationId { get; set; }
        public Boolean isDeleted { get; set; }
    }

    public class UserShow {
        public int Userid { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }
      
    }

}
