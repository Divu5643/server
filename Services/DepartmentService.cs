using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Services
{
    public class DepartmentService:IDepartment
    {
        private readonly AppDbContext? _appDbContext;

        public DepartmentService(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }
        public List<DepartmentMaster> getDepartments() {

            List<DepartmentMaster> deptList = _appDbContext!.DepartmentMasters.Where(obj=> !obj.IsDeleted).ToList();
            return deptList;



        }

        public Boolean AddDepartment(string department)
        {

            DepartmentMaster newDepartment = new DepartmentMaster()
            {
                Department = department
            };
            _appDbContext!.DepartmentMasters.Add(newDepartment);
            _appDbContext.SaveChanges();
            return true;
        }
        public Boolean DeleteDepartment(int departmentId)
        {
            DepartmentMaster? deletedDepartment = _appDbContext!.DepartmentMasters.Find(departmentId);
            if (deletedDepartment == null)
            {
                return false;
            }
            deletedDepartment.IsDeleted = true;
            _appDbContext.Update(deletedDepartment);
            _appDbContext.SaveChanges();
            return true;
        }
        public Boolean Editdepartment(EditDeptModel editDept) {
            DepartmentMaster? editDepartment = _appDbContext!.DepartmentMasters.Find(editDept.departmentId);
            if (editDepartment == null)
            {
                return false;
            }
            editDepartment.Department = editDept.department!;
            _appDbContext.Update(editDepartment);
            _appDbContext.SaveChanges();
            return true;
        }

    }
}
