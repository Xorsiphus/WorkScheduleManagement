using System;

namespace WorkScheduleManagement.Data.Entities.Requests
{
    public abstract class Request : IEntity
    {
        public Guid Id { get; set; }

        public Guid Creator { get; set; }

        public RequestTypes RequestTypes { get; set; }
        
        public RequestStatuses RequestStatuses { get; set; }

        public Guid Approver { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime SentAt { get; set; }

        public string Comment { get; set; }
    }
}