namespace BigProject.Entities
{
    public class Document : EntityBase
    {
        public int UserId {  get; set; }
        public string DocumentName { get; set; }
        public bool IsPublic { get; set; }
        public string FileType { get; set; } 
        public byte[] FileData { get; set; } 
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public User User { get; set; }
    }
}
