using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Controllers
{
    [Route("department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _department;

        public DepartmentController(IDepartment department)
        {
            _department = department;
        }

        [HttpGet("getAllDepartments")]
        public IActionResult getAllDepartments()
        {
            List<DepartmentMaster> deptList = _department.getDepartments();
            return Ok(deptList);
        }

        [HttpPut("AddDepartment")]
        public IActionResult AddDepartment([FromBody] AddDesignationModel department)
        {
            Boolean added = _department.AddDepartment(department.designation!);
            return Ok();
        }

        [HttpPut("DeleteDepartment")]
        public IActionResult DeleteDepartment([FromBody] UserIDModel userId)
        {
            Boolean added = _department.DeleteDepartment(userId.userID);
            return Ok();
        }

        [HttpPut("EditDepartment")]
        public IActionResult Editdepartment([FromBody] EditDeptModel edit)
        {
            Boolean added = _department.Editdepartment(edit);
            if (added)
            {
                return Ok();

            }
            else { return BadRequest(); }
        }

    }
}
