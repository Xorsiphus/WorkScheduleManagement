using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class OverworkingDays : IEntity
    {
        public Guid RequestId { get; set; }

        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
        
        public DayOffInsteadOverworkingRequest Request { get; set; }
    }
}