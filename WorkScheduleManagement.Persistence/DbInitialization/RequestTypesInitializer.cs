using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Persistence.DbInitialization
{
    public class RequestTypesInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            var requestTypes = new List<RequestTypes>
            {
                new RequestTypes {Id = RequestType.OnHoliday, Name = "Заявка на отгул"},
                new RequestTypes {Id = RequestType.OnVacation, Name = "Заявка на отпуск"},
                new RequestTypes {Id = RequestType.OnRemoteWork, Name = "Заявка на удаленную работу"},
                new RequestTypes
                    {Id = RequestType.OnDayOffInsteadOverworking, Name = "Заявка на выходной в счет отработки"},
                new RequestTypes {Id = RequestType.OnDayOffInsteadVacation, Name = "Заявка на выходной в счет отпуска"},
            };

            foreach (var requestType in requestTypes)
            {
                if (await context.RequestTypes.FirstOrDefaultAsync(s => s.Id == requestType.Id) == null)
                {
                    await context.RequestTypes.AddAsync(requestType);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}