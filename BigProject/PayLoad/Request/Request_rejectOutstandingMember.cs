namespace BigProject.PayLoad.Request
{
    public class Request_rejectOutstandingMember
    {
        public int MemberInfoId { get; set; }

        public string? RejectReason { get; set; }
    }
}
