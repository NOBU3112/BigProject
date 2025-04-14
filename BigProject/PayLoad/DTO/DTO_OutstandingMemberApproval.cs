namespace BigProject.PayLoad.DTO
{
    public class DTO_OutstandingMemberApproval
    {
        public int Id { get; set; }
        public string MemberInfoName { get; set; }
        public string MemberInfoMaSV { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public string? RejectReason { get; set; }
        public string HistoryType { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovedByMaSV { get; set; }
        public bool IsAccept { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}
