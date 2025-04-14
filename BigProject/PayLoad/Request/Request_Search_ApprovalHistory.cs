namespace BigProject.PayLoad.Request
{
    public class Request_Search_ApprovalHistory
    {
        public string? MaSV { get; set; }
        public string? HistoryType { get; set; }
        public bool IsAccept { get; set; } = false;
        public bool IsRejecet { get; set; } = false;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
