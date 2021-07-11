using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Models.Models;

namespace WorkScheduleManagement.Models.DAO
{
    public interface IVacationTypesDao
    {
        Task<VacationTypesModel> Get(Guid id);
    }
}