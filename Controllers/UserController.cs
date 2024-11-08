using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly Iuser _userService;

        public UserController(Iuser userService)
        {
            _userService = userService;
        }

        [HttpPost("createUser")]
        public IActionResult createUser([FromBody] UserModel user)
        {

            Boolean newUser = _userService.AddUser(user);
            if (newUser) { return Ok();
            }
            else { return BadRequest("User Already Present"); }
        }
        [HttpPost("updateUser")]
        public IActionResult updateUser([FromBody] UserModel user)
        {

           User? UpdatedUser = _userService.UpdateUser(user);
            if (UpdatedUser == null) { return BadRequest("The user is not present."); }
            return Ok(UpdatedUser);
        }
        [HttpGet("getManagers")]
        public IActionResult getAllManager()
        {
            List<User> managerList = _userService.getManagers();
            return Ok(managerList);
        }
        [HttpGet("getAllUsers")]
        public IActionResult getAllUsers()
        {
            List<User> userList = _userService.getAllUsers();
            return Ok(userList);
        }
        [HttpGet("getAllEmployee")]
        public IActionResult getAllEmployee()
        {
            List<User> employeeList = _userService.getEmployees();
            return Ok(employeeList);
        }
        [HttpGet("getAllEmployeeForReview")]
        public IActionResult getAllEmployeeForReview()
        {
            List<User> employeeList = _userService.getEmployeesForReview();
            return Ok(employeeList);
        }
        [HttpGet("getAllManager")]
        public IActionResult getAllMangaer()
        {
            List<User> employeeList = _userService.getManagers();
            return Ok(employeeList);
        }

        [HttpPost("getSingleUser")]
        public IActionResult getSingleUser([FromBody] UserIDModel userID)
        {

            User? user = _userService.getUser(userID.userID);

            if (user != null) { return Ok(user); }
            else { return NotFound("The user Serched was not in Database"); }
        }

        [HttpPost("deleteUser")]
        public IActionResult DeleteUser(UserIDModel userID)
        {

            Boolean Deleted = _userService.MarkAsDeleted(userID.userID);
            if (Deleted)
            {
                return Ok("The User was deleted");
            }

            else
            {
                return BadRequest("The user id is not present");
            }


        }
        [HttpPost("Loginuser")]
        public IActionResult Loginuser([FromBody] LoginModel loginDetails ) {

            CheckResponseModel result =  _userService.checkUser(loginDetails);
            if (result.success) {
                return Ok(result.loggedInUser);
            }
            else
            {
                return BadRequest(result.responseMessage);
            }

        }
        [HttpGet("employeePieChart")]
        public IActionResult EmployeePieChart()
        {

            List<EmployeePieChart>  employeeData= _userService.getDataForEmployeePieChart();
            return Ok(employeeData);
        }


        [HttpPost("employeePieChartByManger")]
        public IActionResult EmployeePieChartByManger([FromBody]UserIDModel user)
        {

            int employeeData = _userService.getDataForEmployeePieChartByManager(user.userID);
            return Ok(employeeData);
        }

        [HttpPost("getRowsForTablePagination")]
        public IActionResult getRowsForTablePagination([FromBody] Pagination pageNum) {

            List <User> paginationRows = _userService.getPaginationRows(pageNum);
            if(paginationRows.Count == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(paginationRows);
            }
        }

        [HttpPost("getRowsForSearchAndPagination")]
        public IActionResult getRowsForSearchAndPagination([FromBody] Pagination pageNum)
        {

            List<User> paginationRows = _userService.getRowsForSearchAndPagination(pageNum);
           
                return Ok(paginationRows);
            

        }

        [HttpPost("getTotalCount")]
        public IActionResult getTotalCount([FromBody] Pagination pageNum) {
            int totalRowCount = _userService.getTotalRowCount(pageNum);
            return Ok(totalRowCount);
        }
    }
}
