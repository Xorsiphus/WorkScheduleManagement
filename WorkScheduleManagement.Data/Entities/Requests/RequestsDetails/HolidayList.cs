using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class HolidayList
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public HolidayRequest Request { get; set; }
    }
}