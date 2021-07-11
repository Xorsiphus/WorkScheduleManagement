using System;
using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadOverworkingRequest : Request
    {
        public Guid Replacer { get; set; }
    }
}