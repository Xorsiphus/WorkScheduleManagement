using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class OverworkingDays : IEntity
    {
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }
        
        public DateTime DateTo { get; set; }
        
        public DayOffInsteadOverworkingRequest Request { get; set; }
    }
}