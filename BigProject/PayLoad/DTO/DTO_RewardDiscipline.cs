using BigProject.Entities;
using BigProject.Enums;

namespace BigProject.PayLoad.DTO
{
    public class DTO_RewardDiscipline
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public RequestEnum Status { get; set; }
        //public string RewardDisciplineType { get; set; }
        public bool RewardOrDiscipline { get; set; }
        public string? RejectReason { get; set; }
        public string Class { get; set; }
        public string UrlFile { get; set; }
        public string ProposerName { get; set; }
        public string ProposerMaSV { get; set; }  
    }
}
