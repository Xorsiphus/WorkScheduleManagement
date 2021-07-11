using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Models.DAO.Requests;
using WorkScheduleManagement.Models.Models.Requests;

namespace WorkScheduleManagement.Models.Services.Requests
{
    public class RemoteWorkRequestService // : IRemoteWorkRequestDao
    {
        // private readonly IDbRepository _service;
        // private readonly IMapper _mapper;
        //
        // public RemoteWorkRequestService(IDbRepository service, IMapper mapper)
        // {
        //     _service = service;
        //     _mapper = mapper;
        // }
        //
        // public async Task<RemoteWorkRequestModel> Get(Guid id)
        // {
        //     var entity = await _service
        //         .Get<RemoteWorkRequest>(t => t.Id == id)
        //         .FirstOrDefaultAsync();
        //
        //     if (entity == null)
        //         return null;
        //     
        //     var model = _mapper.Map<RemoteWorkRequestModel>(entity);
        //
        //     return model;
        // }
        //
        // public async Task<RemoteWorkRequestModel> Create(RemoteWorkRequestModel requestModel)
        // {
        //     var entity = _mapper.Map<RemoteWorkRequest>(requestModel);
        //     
        //     if (await _service.Add(entity) == null)
        //         return null;
        //     
        //     await _service.Save();
        //
        //     return requestModel;
        // }
        //
        // public async Task<RemoteWorkRequestModel> Update(RemoteWorkRequestModel taskModel)
        // {
        //     var entity = await _service
        //         .Get<RemoteWorkRequest>(t => t.Id == taskModel.Id)
        //         .FirstOrDefaultAsync();
        //
        //     if (entity == null)
        //         return null;
        //
        //     return taskModel;
        // }
    }
}