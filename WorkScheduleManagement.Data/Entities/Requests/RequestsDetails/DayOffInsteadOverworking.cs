using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class DayOffInsteadOverworking
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DayOffInsteadOverworkingRequest Request { get; set; }
    }
}