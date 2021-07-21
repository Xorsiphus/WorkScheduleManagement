using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class DaysInsteadVacation
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DayOffInsteadVacationRequest Request { get; set; }
    }
}