using Microsoft.EntityFrameworkCore;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;
using System.Runtime.CompilerServices;
using BCrypt.Net;
using System.Collections.Generic;
namespace perfomanceSystemServer.Services
{
    public class UserService : Iuser
    {
        private readonly AppDbContext? _appDbContext;
        public UserService()
        {

        }
        public UserService(AppDbContext? appDbContext) {

            _appDbContext = appDbContext;
        }
        public Boolean AddUser(UserModel user)
        {
            try
            {
                //if (user.designation == "" || user.designation==null)
                //{
                //    user.designation = "trainee";
                //}
                String hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.password);
                User newUser = new User()
                {
                    Name = user.name,
                    DeptId = user.deptId,
                    DesignationId = user.designationId,
                    Email = user.email!,
                    Password = hashedPassword,
                    RoleId = user.roleId!,
                };
                _appDbContext!.Add(newUser);

                _appDbContext.SaveChanges();
                EmployeeDetail newEmp = new EmployeeDetail()
                {
                    Userid = newUser.Userid,
                };
                _appDbContext.EmployeeDetails.Add(newEmp);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);

                return false;
            }
        }

        public List<User> getAllUsers()
        {   
            List<User> userList = (_appDbContext!.Users
                .Where(user => user.RoleId!=1 && !user.IsDeleted )).ToList();
            return userList;
        }

        public List<User> getEmployees()
        {
            List<User> employeeList = _appDbContext!.Users
                .Where(user => user.RoleId== 3 && !user.IsDeleted).ToList();
            return employeeList;
        }
        public List<User> getEmployeesForReview()
        {
            //List<User> employeeList = (from user in _appDbContext!.Users
            //                           join review in _appDbContext.Reviewers on user.Userid equals review.EmployeeId
            //                           into reviewGroup
            //                           from review in reviewGroup.DefaultIfEmpty()
            //                           where user.RoleId==3 && review == null 
            //                           select user).ToList();

            List < User > employeeList = (from user in _appDbContext.Users 
            where user.RoleId == 3 && !_appDbContext.Reviewers
            .Where(reviewer => !reviewer.IsDeleted )
            .Select(reviewer => reviewer.EmployeeId).Contains(user.Userid) select user).ToList();

            return employeeList;
        }

        public List<User> getManagers()
        {
            List<User> managerList = _appDbContext!.Users
                .Where(user => user.RoleId == 2 && !user.IsDeleted).ToList();
            return managerList;

        }
        public List<User> getAdmins()
        {
            List<User> managerList = _appDbContext!.Users
                .Where(user => user.RoleId == 1 && !user.IsDeleted).ToList();
            return managerList;

        }
        public User? getUser(int userId) {
            User? user = _appDbContext!.Users
                .Where(user=>!user.IsDeleted ).FirstOrDefault(user=>user.Userid==userId);
            return user;
        }
        public Boolean MarkAsDeleted(int userID)
        {
            User? user = _appDbContext!.Users.Find(userID);
            if (user!.RoleId == 2) {
                (from review in _appDbContext!.Reviewers
                 where review.ManagerId ==  user.Userid
                 select review)
                    .ToList() 
                     .ForEach(x => x.IsDeleted = true);

            }
            else if (user.RoleId == 3)
            {
                //delete the manager for employee
                (from review in _appDbContext!.Reviewers
                 where review.EmployeeId == user.Userid && review.IsDeleted == false && user.IsDeleted == false
                 select review)
                     .ToList()
                      .ForEach(obj => obj.IsDeleted =true );
                //delete the goals of the employee
                (from goal in _appDbContext.Goals
                 where goal.UserId == user.Userid
                 select goal)
                    .ToList()
                     .ForEach(x => x.IsDeleted = true);

            }
            if (user==null) { return false; }

            user.IsDeleted = true;
            _appDbContext.Update(user);
            _appDbContext.SaveChanges();
            return true;

        }
        public User? UpdateUser(UserModel user) {
            var oldUser = _appDbContext!.Users.Find(user.userid);
            if (oldUser == null) { return null; }
            if(oldUser.RoleId != user.roleId)
            {

            if (oldUser.RoleId == 2 ) {
                (from review in _appDbContext.Reviewers
                 where review.ManagerId == oldUser.Userid
                 select review)
                   .ToList()
                    .ForEach(x => x.IsDeleted = true);
            }else if(oldUser.RoleId == 3)
            {
                //delete the manager for employee
                var deletedReview = (from review in _appDbContext.Reviewers
                                     where review.EmployeeId == user.userid
                                     select review).FirstOrDefault();
                if (deletedReview != null) { deletedReview.IsDeleted = true; }
                //delete the goals of the employee
                (from goal in _appDbContext.Goals
                 where goal.UserId == user.userid
                 select goal)
                    .ToList()
                     .ForEach(x => x.IsDeleted = true);
            }
            }
            oldUser.Name = user.name;
            oldUser.Email = user.email!;
            oldUser.Password= user.password!;
            oldUser.DeptId = user.deptId;
            oldUser.RoleId = user.roleId!;
            oldUser.DesignationId = user.designationId!;
            _appDbContext!.Update(oldUser);
            _appDbContext.SaveChanges();
            return oldUser;
        }
        public CheckResponseModel checkUser(LoginModel loginDetails)
        {
            try
            {
                CheckResponseModel result = new CheckResponseModel();
                User? loggedInUser = _appDbContext!.Users.
                    Where(obj => obj.Email == loginDetails.email && !obj.IsDeleted).FirstOrDefault();
                var Checkpass = BCrypt.Net.BCrypt.Verify(loginDetails.password, loggedInUser!.Password);
                if (loggedInUser != null)
                {
                    if (Checkpass)
                    {
                        result.success = true;
                        result.loggedInUser = loggedInUser;
                    }
                    else
                    {
                        result.success = false;
                        result.responseMessage = "The Password entered is Incorrect.";
                    }

                }
                else
                {
                    result.responseMessage = "The email entered is not registered.";
                    result.success = false;
                }
                return result;
            }catch(Exception e)
            {
                return new CheckResponseModel();
            }
        }
        public List<EmployeePieChart> getDataForEmployeePieChart()
        {
            List<EmployeePieChart> chartData = _appDbContext!.Users.Join(_appDbContext.RoleMasters,
                user=> user.RoleId,
                role => role.RoleId,
                (user,role) =>new {user,role})
                .GroupBy(x => x.role.Role).Select(data => new EmployeePieChart
            {
                employeeRole = data.Key,
                totalEmployee = data.Count()
            }).ToList();
                        
            return chartData;
        }
        public int getDataForEmployeePieChartByManager(int managerId)
        {
            int chartData = _appDbContext!.Reviewers
                .Where(obj => obj.ManagerId == managerId && !obj.IsDeleted).Count();
            return chartData;
        }

        public List<User> getPaginationRows(Pagination pageNum) {
            int skipRows = pageNum.PageNumber * pageNum.RowCount;
            List<User> paginationRows;
            if (pageNum.RoleId == 1)
            {
                paginationRows = _appDbContext!.Users
                                             .Where(obj => !obj.IsDeleted && obj.RoleId != 1)
                                             .Skip(skipRows).Take(pageNum.RowCount).ToList();
            }
            else
            {

             paginationRows = _appDbContext!.Users
                                            .Where(obj=>!obj.IsDeleted && obj.RoleId == pageNum.RoleId)
                                            .Skip(skipRows).Take(pageNum.RowCount).ToList();
            }
                                               

        return paginationRows;
        }

        public List<User> getRowsForSearchAndPagination(Pagination pageNum)
        {
            List<User> paginationRows;
            if (pageNum.Search == "")
            {
                paginationRows =  getPaginationRows(pageNum);
            }
            else
            {
                if (pageNum.RoleId == 1)
                {
                    paginationRows = _appDbContext!.Users
                                    .Where(obj => obj.Name!.ToLower().Contains(pageNum.Search!.ToLower())
                                    && obj.RoleId != 1)
                                    .Take(pageNum.RowCount)
                                    .ToList();
                                   
                }
                else
                {
                    paginationRows = _appDbContext!.Users
                                    .Where(obj => obj.Name!.ToLower().Contains(pageNum.Search!.ToLower())
                                    && obj.RoleId == pageNum.RoleId)
                                    .Take(pageNum.RowCount)
                                    .ToList();
                }
            }
            return paginationRows;
        }

        public int getTotalRowCount(Pagination pageNum)
        {
            int totalRowCount = 0;
            if (pageNum.Search == "")
            {
                if (pageNum.RoleId == 1)
                {
                    totalRowCount = _appDbContext!.Users
                                  .Where(obj => !obj.IsDeleted && obj.RoleId != 1).Count();
                }
                else
                {
                    totalRowCount = _appDbContext!.Users
                                        .Where(obj => !obj.IsDeleted && obj.RoleId == pageNum.RoleId).Count();

                }
            }
            else
            {
                totalRowCount = getRowsForSearchAndPagination(pageNum).Count;
            }
            return totalRowCount;
        }
    }
}
