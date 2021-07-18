using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Data.Entities
{
    public class RequestStatuses : IEntity
    {
        public RequestStatus Id { get; set; }
        
        public string Name { get; set; }
    }
}