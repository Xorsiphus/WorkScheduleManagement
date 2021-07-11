using System;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public class DayOffInsteadVacationRequest : Request
    {
        public Guid Replacer { get; set; }
    }
}