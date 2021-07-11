using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Models.Models.Requests;

namespace WorkScheduleManagement.Models.DAO.Requests
{
    public interface IVacationRequestDao
    {
        Task<VacationRequestModel> Get(Guid id);
        Task<VacationRequestModel> Create(VacationRequestModel taskModel);
        Task<VacationRequestModel> Update(VacationRequestModel taskModel);
    }
}