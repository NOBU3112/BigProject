namespace BigProject.PayLoad.DTO
{
    public class DTO_Document
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentContent { get; set; }
        public string UrlAvatar { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
