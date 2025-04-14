using BigProject.Enums;

namespace BigProject.PayLoad.DTO
{
    public class DTO_OutstandingMember
    {
        public int Id { get; set; }
        public string MemberInfoName { get; set; }
        public string MemberInfoMaSV {  get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public string? RejectReason { get; set; }
    }
}
