﻿using BigProject.Enums;

namespace BigProject.PayLoad.DTO
{
    public class DTO_EventJoin
    {
        public string EventName { get; set; }
        public string FullName { get; set; }
        public string MaSV { get; set; }
        public string Class {  get; set; }
        public EventJointEnum Status { get; set; } = EventJointEnum.registered;
    }
}
