using System;
using System.Collections.Generic;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class HolidayRequest : Request
    {
        public ApplicationUser Replacer { get; set; }
        
        public IList<HolidayList> HolidayList { get; set; }
    }
}