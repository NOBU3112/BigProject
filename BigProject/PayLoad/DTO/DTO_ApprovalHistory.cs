namespace BigProject.PayLoad.DTO
{
    public class DTO_ApprovalHistory
    {
        public int Id { get; set; }
        public int? RequestToBeOutstandingMemberId { get; set; }
        public int? RewardDisciplineId { get; set; }
        public string HistoryType { get; set; }
        public int ApprovedById { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovedByMaSV { get; set; }
        public bool IsAccept { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string memberMaSV {  get; set; }
        public string MemberName { get; set; }
        public string? description {  get; set; }
        public string? rejectReason { get; set; }
        //public string? RejectReason { get; set; }
    }
}
