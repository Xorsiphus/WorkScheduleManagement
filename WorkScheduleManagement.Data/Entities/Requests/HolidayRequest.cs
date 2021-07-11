using System;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class HolidayRequest : Request
    {
        public Guid Replacer { get; set; }
    }
}