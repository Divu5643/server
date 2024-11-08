using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Interface
{
    public interface IDepartment
    {
        public List<DepartmentMaster> getDepartments();
        public Boolean AddDepartment(string department);
        public Boolean DeleteDepartment(int departmentId);
        public Boolean Editdepartment(EditDeptModel departmentId);
    }
}
