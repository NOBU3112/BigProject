using BigProject.Enums;

namespace BigProject.Entities
{
    public class RewardDiscipline : EntityBase
    {
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool RewardOrDiscipline { get; set; }
        public RequestEnum Status { get; set; }
        public string? RejectReason { get; set; }
        //public int RewardDisciplineTypeId { get; set; }
        public int RecipientId { get; set; }
        public User Recipient { get; set; } 
        public int ProposerId { get; set; }
        public User Proposer { get; set; }
        //public RewardDisciplineType RewardDisciplineType { get; set; }
    }
}
