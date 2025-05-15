using BigProject.Enums;

namespace BigProject.PayLoad.Request
{
    public class Request_Search_RewardDiscipline
    {
        public string? Description { get; set; }  
        public int? CreateDay { get; set; }
        public int? CreateMonth { get; set; }
        public int? CreateYear { get; set; }
        public string? Status { get; set; } // 0-waiting,1-accept,2-reject,
        public string? Class { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
