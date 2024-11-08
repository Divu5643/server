namespace perfomanceSystemServer.ResponseModels
{
   
    public class AddPerformance{

        public int userId {  get; set; }
        public int technicalSkill {  get; set; }
        public int softSkill { get; set; }
        public int teamworkSkill { get; set; }
        public int deliveryTime { get; set; }
        public string? remark { get; set; }
        public int createdBy { get; set; }
    
    }
}
