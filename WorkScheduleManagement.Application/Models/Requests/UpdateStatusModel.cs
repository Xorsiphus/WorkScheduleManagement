using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Application.Models.Requests
{
    public class UpdateStatusModel
    {
        public string Id { get; set; }
        
        public RequestType Type { get; set; }
        
        public RequestStatus NewStatus { get; set; }
    }
}