﻿namespace BigProject.PayLoad.Request
{
    public class Request_UpdateMemberInfo
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public DateTime Birthdate { get; set; }
        public string FullName { get; set; }
        public string MemberId { get; set; }
        public string Nation { get; set; }
        public string religion { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? UrlAvatar { get; set; }
        public string? PoliticalTheory { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PlaceOfJoining { get; set; }
        public int UserId { get; set; }
    }
}
