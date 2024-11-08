using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SignalR;
using perfomanceSystemServer.Interface;
using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Controllers
{
    [Route("/reviewer")]
    [ApiController]

    public class ReviewController : ControllerBase
    {
        private readonly Ireviewer _reviewer;
        public ReviewController(Ireviewer reviewer)
        {
            _reviewer = reviewer;
        }

        [HttpPost("addReviewer")]
        public IActionResult AddReviewer(ReviewerModel review) {
          
            Reviewer AddedReviewer = _reviewer.AddReviewer(review)!;

            if (AddedReviewer != null) { return Ok(AddedReviewer); }
            else { return BadRequest(AddedReviewer); }
        }
        [HttpPost("getReviews")]
        public IActionResult Getreviews( [FromBody]ReviewPagination paginationValues)
        {
            paginationEmployeeAndManager reviewList = _reviewer.getReviewsWithPagination(paginationValues);
            return Ok(reviewList);
        }

        [HttpPut("deleteReview")]
        public IActionResult DeleteReview([FromBody] IreviewId reviewId ){

            Reviewer? deletedOne = _reviewer.DeleteReviewer(reviewId.reviewId);
            if (deletedOne != null)
            {
                return Ok(deletedOne);
            }
            else { return BadRequest(); }
            
        }
        [HttpPost("getUserByManager")]
        public IActionResult getUserByManager([FromBody] UserIDModel userId)
        {
            List<IuserByManager> usersByManager = _reviewer.getUserByManager(userId.userID);
            return Ok(usersByManager);
        }
        [HttpGet("getReviewHistory")]
        public IActionResult getReviewHistory(int userId)
        {
            List<EmployeeAndManager> reviewerList = _reviewer.getReviewerHistory(userId);
            return Ok(reviewerList);
        }
    }
}
