using perfomanceSystemServer.Models;

namespace perfomanceSystemServer.ResponseModels
{
    public class LoginModel
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }

    public class CheckResponseModel
    {
        public User? loggedInUser { get; set; }
        public string? responseMessage { get; set; }
        public Boolean success { get; set; }
    }
}
