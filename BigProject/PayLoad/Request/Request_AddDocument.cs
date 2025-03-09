namespace BigProject.PayLoad.Request
{
    public class Request_AddDocument
    {
        public string DocumentTitle { get; set; }
        public string DocumentContent { get; set; }
        public IFormFile? UrlAvatar { get; set; }
    }
}
