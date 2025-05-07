using BigProject.Entities;

namespace BigProject.PayLoad.Request
{
    public class Request_ProposeRewardDiscipline
    {
        public string Description { get; set; }
        //public int RewardDisciplineTypeId { get; set; }
        public string Class {  get; set; }
        public IFormFile Url {  get; set; }
    }
}
