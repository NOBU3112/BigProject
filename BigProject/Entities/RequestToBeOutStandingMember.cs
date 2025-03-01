﻿namespace BigProject.Entities
{
    public class RequestToBeOutStandingMember : EntityBase
    {
        public int MemberInfoId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public string? RejectReason { get; set; }
        public MemberInfo MemberInfo { get; set; }
    }
}
