using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;
using System.Numerics;
using System.Reflection;

namespace perfomanceSystemServer.Services
{
    public class ProfileService:IProfile
    {
        private readonly AppDbContext _appDbContext;
        public ProfileService(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }
        public EmployeeProfile? getProfile(int userId)
        {

            EmployeeProfile? employeeProfile = (from details in _appDbContext.EmployeeDetails
                                                join user in _appDbContext.Users on details.Userid equals user.Userid
                                                join role in _appDbContext.RoleMasters on user.RoleId equals role.RoleId
                                                join dept in _appDbContext.DepartmentMasters on user.DeptId equals dept.DeptId
                                                join desg in _appDbContext.DesignationMasters on user.DesignationId equals desg.DesignationId
                                                join review in _appDbContext.Reviewers
                                                .Where(r =>!r.IsDeleted) on user.Userid equals review.EmployeeId into reviewGroup
                                                from review in reviewGroup.DefaultIfEmpty()
                                                join manager in _appDbContext.Users on review.ManagerId equals manager.Userid into managerGroup
                                                from manager in managerGroup.DefaultIfEmpty()
                                                where details.Userid == userId 
                                                select new EmployeeProfile
                                                {
                                                    userId = (int)details.Userid!,
                                                    name = user.Name ?? "",
                                                    designation = desg.Designation,
                                                    department = dept.Department,
                                                    reportingManager = manager.Name,
                                                    dateOfBirth = (DateOnly)details.DateofBirth!,
                                                    gender = details.Gender?? "",
                                                    email = user.Email,
                                                    phone = details.Phone ?? "",
                                                    role = role.Role,
                                                    personalEmail = details.PersonalEmail ?? "",
                                                }).FirstOrDefault();
            return employeeProfile;
            
        }
        public EmployeeProfile updateOrCreateProfile(EmployeeProfile profile)
        {
            User? updateuser = _appDbContext.Users.Find(profile.userId);
            updateuser!.Name = profile.name!.ToLower();
            EmployeeDetail employeeDetail = _appDbContext.EmployeeDetails
                .Where(emp => emp.Userid == profile.userId).FirstOrDefault() ?? new EmployeeDetail();

            employeeDetail.Userid = profile.userId;
            employeeDetail.DateofBirth = profile.dateOfBirth;
            employeeDetail.Gender = profile.gender;
            employeeDetail.PersonalEmail = profile.personalEmail;
            employeeDetail.Phone = profile.phone;

            _appDbContext.Users.Update(updateuser);
            _appDbContext.EmployeeDetails.Update(employeeDetail);
            _appDbContext.SaveChanges();
            return profile;
        }
    }
}
