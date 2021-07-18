using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Persistence.DbInitialization
{
    public class VacationTypesInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            var vacationTypes = new List<VacationTypes>
            {
                new VacationTypes {Id = VacationType.MaternityVacation, Name = "Отпуск по беременности и родам (декретный отпуск)"},
                new VacationTypes {Id = VacationType.UnpaidVacation, Name = "Отпуск без сохранения заработной платы"},
                new VacationTypes {Id = VacationType.AdditionalPaidVacation, Name = "Дополнительный оплачиваемый отпуск (в т.ч. учебный)"},
                new VacationTypes {Id = VacationType.BasicPaidVacation, Name = "Основной оплачиваемый отпуск"},
                new VacationTypes {Id = VacationType.VacationToCareForTheChild, Name = "Отпуск по уходу за ребенком"},
            };

            foreach (var vacationType in vacationTypes)
            {
                if (await context.VacationTypes.FirstOrDefaultAsync(s => s.Id == vacationType.Id) == null)
                {
                    await context.VacationTypes.AddAsync(vacationType);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}