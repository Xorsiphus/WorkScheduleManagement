﻿using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Data.Entities
{
    public class RequestTypes : IEntity
    {
        public RequestType Id { get; set; }

        public string Name { get; set; }
    }
}