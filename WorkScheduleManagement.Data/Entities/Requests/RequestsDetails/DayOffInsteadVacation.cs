using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class DayOffInsteadVacation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DayOffInsteadVacationRequest Request { get; set; }
    }
}