

namespace WorkScheduleManagement.Application.Models.Requests
{
    public class ReqeustTableModel
    {
        public string Id { get; set; }

        public string Creator { get; set; }

        public string RequestType { get; set; }
        
        public string RequestStatus { get; set; }

        public string SelectedDates { get; set; }
        
        public int CountOfVacationDays { get; set; }
    }
}