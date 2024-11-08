using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;
using perfomanceSystemServer.Services;

namespace perfomanceSystemServer.Controllers
{
    [Route("goal")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly Igoal _goal;

        public GoalController(Igoal goal) {
            _goal = goal;
        }

        [HttpPost("CreateGoal")]
        public IActionResult CreateGoal([FromBody] NewGoal newGoal) {
            Goal tempGoal = _goal.AddGoal(newGoal);
            return Ok(tempGoal);

        }
        [HttpGet("getGoals")]
        public IActionResult GetGoals()
        {
            List<ShowGoals> activeGoalList = _goal.getAllGoals();
            return Ok(activeGoalList);
        }
        [HttpPut("editGoal")]
        public IActionResult EditGoal(EditGoal changedGoal) {
            Goal? editedGoal = _goal.editGoal(changedGoal);
            if (editedGoal != null)
            {
                return Ok(editedGoal);
            }
            else { return BadRequest(); }

        }
        [HttpPut("deleteGoal")]
        public IActionResult DeleteGoal(int id) {
            Goal? deletedGoal = _goal.DeleteGoal(id);
            if (deletedGoal != null)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }
        [HttpPost("getGoalsForEmployee")]
        public IActionResult getGoalsForEmployee([FromBody] UserIDModel userId) {

            List<ShowGoals> employeeGoals = _goal.goalsForEmloyee(userId.userID);
            return Ok(employeeGoals);

        }
        [HttpPost("markAsInProgress")]
        public IActionResult markAsInProgress([FromBody] UserIDModel userId) {
            Goal? editedGoal = _goal.markAsInProgress(userId.userID);
            if (editedGoal != null)
            {
                return Ok();
            }
            else { return BadRequest(); }
        }
        [HttpPost("getGoalsByManager")]
        public IActionResult getGoalsByManager([FromBody] UserIDModel userId)
        {
            List<ShowGoals> employeeGoals = _goal.goalsByManagerID(userId.userID);
            return Ok(employeeGoals);

        }
        [HttpGet("getGoalDataForChart")]
        public IActionResult getGoalDataForChart()
        {
            List<BarChartResponse> goalData = _goal.getGoalDataForChart();
            return Ok(goalData);
        }

        [HttpGet("getCommentsForGoal")]
        public IActionResult getCommentsForGoal(int goalId)
        {
            List<DisplayComment> CommentList = _goal.getCommentsForGoal(goalId);
            return Ok(CommentList);
        }

        [HttpPost("addCommentToGoal")]
        public IActionResult addCommentToGoal([FromBody] CommentResponse newComment) {
            if (newComment.CommentText == "")
            {
                return BadRequest("No body in Comment");
            }
            Comment? comment = _goal.addComment(newComment);
            return Ok(comment);
        }

        [HttpGet("getSpecificGoal")]
        public IActionResult getSpecificGoal(int goalId) {
            ShowGoals? goal = _goal.getSpecificGoal(goalId);
            return Ok(goal);
        }

        [HttpPost("getAllGoals")]
        public IActionResult getAllGoals([FromBody] GoalPagination pageNum)
        {
            goalPaginationResponse paginationRows = _goal.getGoalsForSearchAndPagination(pageNum);
            return Ok(paginationRows);
        }

        [HttpPost("getAllGoalsforManager")]
        public IActionResult getAllGoalsForManager([FromBody] GoalPagination pageNum)
        {
            goalPaginationResponse paginationRows = _goal.getGoalsForSearchAndPagination(pageNum);
            return Ok(paginationRows);
        }
    }
}
