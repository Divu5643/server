using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Interface
{
    public interface IProfile
    {
        public EmployeeProfile? getProfile(int userId);
        public EmployeeProfile updateOrCreateProfile(EmployeeProfile profile);
    }
}
