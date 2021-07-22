using System;
using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadOverworkingRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
        
        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
        
        public IList<DayOffInsteadOverworking> DaysOffInsteadOverworking { get; set; }
    }
}