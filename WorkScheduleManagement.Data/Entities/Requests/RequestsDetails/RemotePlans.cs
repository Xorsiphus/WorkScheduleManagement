using System;

namespace WorkScheduleManagement.Data.Entities.Requests.RequestsDetails
{
    public class RemotePlans : IEntity
    {
        public int Id { get; set; }
        
        public Guid RequestId { get; set; }

        public DateTime Date { get; set; }
        
        public string WorkingPlan { get; set; }
        
        public RemoteWorkRequest Request { get; set; }
    }
}