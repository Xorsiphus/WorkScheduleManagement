using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Persistence.DbInitialization
{
    public static class RequestStatusesInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            var requestStatuses = new List<RequestStatuses>
            {
                new RequestStatuses {Id = RequestStatus.Agreed, Name = "Согласована"},
                new RequestStatuses {Id = RequestStatus.Approved, Name = "Утверждена"},
                new RequestStatuses {Id = RequestStatus.Canceled, Name = "Отозвана"},
                new RequestStatuses {Id = RequestStatus.New, Name = "Новая"},
                new RequestStatuses {Id = RequestStatus.Rejected, Name = "Отменена"},
                new RequestStatuses {Id = RequestStatus.NotAgreed, Name = "Не согласована"},
                new RequestStatuses {Id = RequestStatus.NotApproved, Name = "Не утверждена"},
                new RequestStatuses {Id = RequestStatus.SentForApproval, Name = "Отправлена на согласование"},
            };

            foreach (var requestStatus in requestStatuses)
            {
                if (await context.RequestStatuses.FirstOrDefaultAsync(s => s.Id == requestStatus.Id) == null)
                {
                    await context.RequestStatuses.AddAsync(requestStatus);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}