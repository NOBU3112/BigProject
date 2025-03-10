namespace BigProject.PayLoad.DTO
{
    public class DTO_ApprovalHistory
    {
        public int Id { get; set; }
        public int? RequestToBeOutstandingMemberId { get; set; }
        public int? RewardDisciplineId { get; set; }
        public string HistoryType { get; set; }
        public int ApprovedById { get; set; }
        public bool IsAccept { get; set; }
        //public string? RejectReason { get; set; }
    }
}
