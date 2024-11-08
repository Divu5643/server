using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Interface
{
    public interface Iuser
    {
        public Boolean AddUser(UserModel user);
        public User? getUser(int userId);
        public List<User> getManagers();
        public List<User> getAllUsers();
        public List<User> getEmployees();
        public List<User> getEmployeesForReview();
        public List<User> getAdmins();
        public bool MarkAsDeleted(int userID);
        public User? UpdateUser(UserModel user);

        public CheckResponseModel checkUser(LoginModel loginDetails);

        public List<EmployeePieChart> getDataForEmployeePieChart();
        public int getDataForEmployeePieChartByManager(int managerId);
        public List<User> getPaginationRows(Pagination pageNum);
        public List<User> getRowsForSearchAndPagination(Pagination pageNum);
        public int getTotalRowCount(Pagination pageNum);


    }
}
