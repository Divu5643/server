using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;

namespace perfomanceSystemServer.Interface
{
    public interface Ireviewer
    {
        public Reviewer? AddReviewer(ReviewerModel review);
        public Reviewer? DeleteReviewer(int reviewID);
        public paginationEmployeeAndManager getReviewsWithPagination(ReviewPagination paginationValues);
        public List<IuserByManager> getUserByManager(int managerId);
        public List<EmployeeAndManager> getReviewerHistory(int userId);
    }
}
