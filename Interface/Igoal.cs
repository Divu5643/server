using perfomanceSystemServer.Models;
using perfomanceSystemServer.ResponseModels;
namespace perfomanceSystemServer.Interface
{
    public interface Igoal
    {
        public Goal AddGoal(NewGoal newGoal);
        public Goal? DeleteGoal(int goalId);
        public List<ShowGoals> getAllGoals();
        public ShowGoals? getSpecificGoal(int goalId);
        public Goal? editGoal(EditGoal changedGoal);

        public List<ShowGoals> goalsForEmloyee(int userId);

        public List<ShowGoals> goalsByManagerID(int managerId);
        public Goal? markAsInProgress(int goalId);
        public List<BarChartResponse> getGoalDataForChart();

        public List<DisplayComment> getCommentsForGoal(int goalId);

        public Comment? addComment(CommentResponse newComment);
        public goalPaginationResponse getGoalsForSearchAndPagination(GoalPagination pageNum);
    }
}
