namespace BigProject.Entities
{
    public class ApprovalHistory : EntityBase
    {
        public int? RequestToBeOutstandingMemberId { get; set; }
        public RequestToBeOutStandingMember? RequestToBeOutstandingMember { get; set; }
        public int? RewardDisciplineId { get; set; }
        public RewardDiscipline? RewardDiscipline { get; set; }
        public int ApprovedById { get; set; }
        public User ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; } = DateTime.Now;
        public string HistoryType { get; set; }
        public bool IsAccept { get; set; }
        //public string? RejectReason { get; set; } // bỏ lý do từ chối
    }
}

