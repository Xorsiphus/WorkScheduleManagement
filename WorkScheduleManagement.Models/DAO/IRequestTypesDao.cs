using System;
using System.Threading.Tasks;
using WorkScheduleManagement.Models.Models;

namespace WorkScheduleManagement.Models.DAO
{
    public interface IRequestTypesDao
    {
        Task<RequestTypesModel> Get(Guid id);
    }
}