﻿namespace BigProject.PayLoad.Request
{
    public class Request_UpdateDocument
    {
        public int Id { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentContent { get; set; }
        public IFormFile? UrlAvatar { get; set; }
    }
}
