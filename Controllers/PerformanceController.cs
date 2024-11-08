using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Controllers
{
    [Route("performance")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {

        private readonly IPerformance _performance;
        public PerformanceController(IPerformance performance) { 
        _performance = performance;
        }

        [HttpPost("savePerformance")]
        public IActionResult savePerformance([FromBody] AddPerformance review)
        {
            
            Performance? result = _performance.savePerformance(review);
            if(result == null) { return BadRequest("The EmployeeId or managerId is Invalid"); }
            else{return Ok(result);}
        }
        [HttpGet("averagePerformance")]
        public IActionResult getaveragePerformance()
        {
            double averageScore = _performance.getAverageScore();
            return Ok(averageScore);
        }

        [HttpPost("averagePerformanceByManager")]
        public IActionResult getaveragePerformancebyManager(UserIDModel userId)
        {
            double averageScore = _performance.getAverageScoreByManager(userId.userID);
            return Ok(averageScore);
        }

        [HttpPost("getPerformanceForEmployee")]
        public IActionResult getPerformanceForEmployee(UserIDModel userId)
        {
            List<Performance> performanceList = _performance.GetPerformance(userId.userID);
            return Ok(performanceList);
        }
        [HttpPost("deletePerformance")]
        public IActionResult deletePerformance(UserIDModel userId)
        {
            Performance deletedPerformance = _performance.markAsDeleted(userId.userID)!;
            if (deletedPerformance == null) { return BadRequest("The performance is not found"); }
            return Ok(deletedPerformance);
        }

        [HttpGet("getBarChartData")]
        public IActionResult getBarChartData() {
            List<PerfomanceBarChartResponse> chartResponse =  _performance.getBarChartData();
            return Ok(chartResponse);
        }
    }
}
