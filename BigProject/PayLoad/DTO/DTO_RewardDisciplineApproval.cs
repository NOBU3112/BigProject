using BigProject.Enums;

namespace BigProject.PayLoad.DTO
{
    public class DTO_RewardDisciplineApproval
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool RewardOrDiscipline { get; set; }
        public string? RejectReason { get; set; }
        public string Class{ get; set; }
        public string UrlFile {  get; set; }
        public string UrlDecodeFile {  get; set; }
        public string ProposerName { get; set; }
        public string ProposerMaSV { get; set; }
        public string HistoryType { get; set; }
        public string ApprovedByName { get; set; }
        public string ApprovedByMaSV { get; set; }
        public bool IsAccept { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}
