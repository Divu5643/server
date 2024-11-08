using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Controllers
{
    [Route("profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfile _profile;

        public ProfileController(IProfile profile)
        {
            _profile = profile;
        }


        [HttpPost("getEmployeeDetails")]
        public IActionResult getEmployeeDetails([FromBody] UserIDModel userId)
        {
            EmployeeProfile? employeeDetails = _profile.getProfile(userId.userID);
            if (employeeDetails == null)
            {
                return BadRequest("User is not created yet.");
            }
            return Ok(employeeDetails);
        }

        [HttpPost("editEmployeeDetails")]
        public IActionResult editEmployeeDetails([FromBody] EmployeeProfile profile)
        {
            EmployeeProfile emp =  _profile.updateOrCreateProfile(profile);
            return Ok(emp);
        }
    }
}
