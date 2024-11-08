using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Controllers
{
    [Route("designation")]
    [ApiController]
    public class DesignationController : ControllerBase
    {

        private readonly IDesignation _designation;

        public DesignationController(IDesignation designation)
        {
            _designation = designation;
        }

        [HttpGet("getAllDesignation")]
        public IActionResult getAllDesignations()
        {
            List<DesignationMaster> desgList = _designation.getDesignations();
            return Ok(desgList);
        }

        [HttpPut("AddDesignation")]
        public IActionResult AddDesignation([FromBody] AddDesignationModel designation) {
            Boolean added =  _designation.addDesignation(designation.designation!);
            return Ok();
        }

        [HttpPut("DeleteDesignation")]
        public IActionResult DeleteDesignation([FromBody] UserIDModel userId)
        {
            Boolean added = _designation.DeleteDesignation(userId.userID);
            return Ok();
        }
    }
}

