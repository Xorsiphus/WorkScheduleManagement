using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleManagement.Application.CQRS.Commands;
using WorkScheduleManagement.Application.CQRS.Queries;
using WorkScheduleManagement.Application.Models.Requests;
using WorkScheduleManagement.Application.Models.Users;
using WorkScheduleManagement.Data.Entities;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Data.Entities.Requests.RequestsDetails;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IMediator _mediator;

        public RequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index() =>
            View(await _mediator.Send(new GetRequestTableModels.Query(User.FindFirstValue(ClaimTypes.NameIdentifier))));

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create() => View(new CreationModel
        {
            AllTypes = await _mediator.Send(new GetRequestTypes.Query()),
            AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query()),
            AllReplacerUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Union(await _mediator.Send(new GetUsersWithRole.Query("employee")))
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}}),
            AllApproverUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}}),
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today
        });

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreationModel model)
        {
            if (ModelState.IsValid)
            {
                Request newRequest;
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                switch (model.Type)
                {
                    case RequestType.OnHoliday:
                        newRequest = new HolidayRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            Approver = await _mediator.Send(new GetUserById.Query(model.Approver)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                            HolidayList = model.CustomDays.ToList().ConvertAll(d => new HolidayList {Date = d}),
                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer))
                        };
                        break;
                    case RequestType.OnRemoteWork:
                        newRequest = new RemoteWorkRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            Approver = await _mediator.Send(new GetUserById.Query(model.Approver)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                            RemotePlans = model.CustomDays.Zip(model.RemotePlans,
                                (time, plan) => new RemotePlans {Date = time, WorkingPlan = plan}).ToList()
                        };
                        break;
                    case RequestType.OnDayOffInsteadOverworking:
                        newRequest = new DayOffInsteadOverworkingRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            Approver = await _mediator.Send(new GetUserById.Query(model.Approver)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                            DateFrom = model.DateFrom,
                            DateTo = model.DateTo,
                            DaysOffInsteadOverworking = model.CustomDays.ToList()
                                .ConvertAll(d => new DayOffInsteadOverworking {Date = d}),
                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                        };
                        break;
                    case RequestType.OnDayOffInsteadVacation:
                        newRequest = new DayOffInsteadVacationRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            Approver = await _mediator.Send(new GetUserById.Query(model.Approver)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                            DaysOffInsteadVacation = model.CustomDays.ToList()
                                .ConvertAll(d => new DayOffInsteadVacation {Date = d}),
                        };
                        break;
                    case RequestType.OnVacation:
                        newRequest = new VacationRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            Approver = await _mediator.Send(new GetUserById.Query(model.Approver)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                            DateFrom = model.DateFrom,
                            DateTo = model.DateTo,
                            VacationType =
                                await _mediator.Send(new GetRequestVacationTypeById.Query(model.VacationType)),
                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                            IsShifting = model.IsShifting
                        };
                        break;
                    default:
                        newRequest = null;
                        break;
                }

                var result = await _mediator.Send(new CreateRequest.Command(newRequest));

                if (result)
                {
                    return RedirectToAction("Index");
                }

                // foreach (var error in )
                // {
                //     ModelState.AddModelError(string.Empty, error.Description);
                // }
            }

            model.AllTypes = await _mediator.Send(new GetRequestTypes.Query());
            model.AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query());
            model.AllReplacerUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Union(await _mediator.Send(new GetUsersWithRole.Query("employee")))
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}});
            model.AllApproverUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}});

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var request = await _mediator.Send(new GetRequestById.Query(id));
            if (request == null)
                return NotFound();

            var requestModel = new CreationModel
            {
                Id = request.Id.ToString(),
                Type = request.RequestTypes.Id,
                Comment = request.Comment,
                Approver = request.Approver?.Id,
                Status = request.RequestStatus.Id,

                AllTypes = await _mediator.Send(new GetRequestTypes.Query()),
                AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query()),
                AllReplacerUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                    .Union(await _mediator.Send(new GetUsersWithRole.Query("employee")))
                    .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}}),
                AllApproverUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                    .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}}),
            };

            switch (request.RequestTypes.Id)
            {
                case RequestType.OnHoliday:
                    requestModel.Replacer = ((HolidayRequest) request).Replacer?.Id;
                    requestModel.CustomDays = ((HolidayRequest) request).HolidayList.ToList().ConvertAll(d => d.Date);
                    requestModel.DateFrom = DateTime.Today;
                    requestModel.DateTo = DateTime.Today;
                    break;
                case RequestType.OnVacation:
                    requestModel.Replacer = ((VacationRequest) request).Replacer?.Id;
                    requestModel.VacationType = ((VacationRequest) request).VacationType.Id;
                    requestModel.DateFrom = ((VacationRequest) request).DateFrom;
                    requestModel.DateTo = ((VacationRequest) request).DateTo;
                    break;
                case RequestType.OnRemoteWork:
                    requestModel.CustomDays =
                        ((RemoteWorkRequest) request).RemotePlans.ToList().ConvertAll(d => d.Date);
                    requestModel.RemotePlans = ((RemoteWorkRequest) request).RemotePlans.ToList()
                        .ConvertAll(d => d.WorkingPlan);
                    requestModel.DateFrom = DateTime.Today;
                    requestModel.DateTo = DateTime.Today;
                    break;
                case RequestType.OnDayOffInsteadOverworking:
                    requestModel.Replacer = ((DayOffInsteadOverworkingRequest) request).Replacer?.Id;
                    requestModel.DateFrom = ((DayOffInsteadOverworkingRequest) request).DateFrom;
                    requestModel.DateTo = ((DayOffInsteadOverworkingRequest) request).DateTo;
                    requestModel.CustomDays = ((DayOffInsteadOverworkingRequest) request).DaysOffInsteadOverworking
                        .ToList().ConvertAll(d => d.Date);
                    break;
                case RequestType.OnDayOffInsteadVacation:
                    requestModel.Replacer = ((DayOffInsteadVacationRequest) request).Replacer?.Id;
                    requestModel.CustomDays = ((DayOffInsteadVacationRequest) request).DaysOffInsteadVacation.ToList()
                        .ConvertAll(d => d.Date);
                    requestModel.DateFrom = DateTime.Today;
                    requestModel.DateTo = DateTime.Today;
                    break;
                default:
                    return NotFound();
            }

            return View(requestModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CreationModel model)
        {
            if (ModelState.IsValid)
            {
                bool result;

                switch (model.Type)
                {
                    case RequestType.OnHoliday:
                        var holidayRequest = new HolidayRequest
                        {
                            Id = new Guid(model.Id),
                            Comment = model.Comment,
                            Approver = await _mediator.Send(new GetUserById.Query(model.Approver)),

                            HolidayList = model.CustomDays.ToList().ConvertAll(d => new HolidayList {Date = d}),
                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                        };
                        result = await _mediator.Send(new UpdateHolidayRequest.Command(holidayRequest));
                        break;
                    case RequestType.OnRemoteWork:
                        var remoteWorkRequest = new RemoteWorkRequest
                        {
                            Id = new Guid(model.Id),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                            RemotePlans = model.CustomDays.Zip(model.RemotePlans,
                                (time, plan) => new RemotePlans {Date = time, WorkingPlan = plan}).ToList()
                        };
                        result = await _mediator.Send(new UpdateRemoteWorkRequest.Command(remoteWorkRequest));
                        break;
                    case RequestType.OnDayOffInsteadOverworking:
                        var dayOffInsteadOverworkingRequest = new DayOffInsteadOverworkingRequest
                        {
                            Id = new Guid(model.Id),
                            Comment = model.Comment,

                            DateFrom = model.DateFrom,
                            DateTo = model.DateTo,
                            DaysOffInsteadOverworking = model.CustomDays.ToList()
                                .ConvertAll(d => new DayOffInsteadOverworking {Date = d}),
                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                        };
                        result = await _mediator.Send(
                            new UpdateDayOffInsteadOverworkingRequest.Command(dayOffInsteadOverworkingRequest));
                        break;
                    case RequestType.OnDayOffInsteadVacation:
                        var dayOffInsteadVacationRequest = new DayOffInsteadVacationRequest
                        {
                            Id = new Guid(model.Id),
                            Comment = model.Comment,

                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                            DaysOffInsteadVacation = model.CustomDays.ToList()
                                .ConvertAll(d => new DayOffInsteadVacation {Date = d}),
                        };
                        result = await _mediator.Send(
                            new UpdateDayOffInsteadVacationRequest.Command(dayOffInsteadVacationRequest));
                        break;
                    case RequestType.OnVacation:
                        var vacationRequest = new VacationRequest
                        {
                            Id = new Guid(model.Id),
                            Comment = model.Comment,

                            DateFrom = model.DateFrom,
                            DateTo = model.DateTo,
                            VacationType =
                                await _mediator.Send(new GetRequestVacationTypeById.Query(model.VacationType)),
                            Replacer = await _mediator.Send(new GetUserById.Query(model.Replacer)),
                            IsShifting = model.IsShifting
                        };
                        result = await _mediator.Send(new UpdateVacationRequest.Command(vacationRequest));
                        break;
                    default:
                        result = false;
                        break;
                }

                if (result)
                {
                    return RedirectToAction("Index");
                }

                // foreach (var error in )
                // {
                //     ModelState.AddModelError(string.Empty, error.Description);
                // }
            }

            model.AllTypes = await _mediator.Send(new GetRequestTypes.Query());
            model.AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query());
            model.AllReplacerUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Union(await _mediator.Send(new GetUsersWithRole.Query("employee")))
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}});
            model.AllApproverUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}});

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(UpdateStatusModel model)
        {
            await _mediator.Send(new UpdateRequestStatus.Command(new VacationRequest
                    {Id = new Guid(model.Id), RequestStatus = new RequestStatuses {Id = model.NewStatus}}));

            return RedirectToAction("Index");
        }
    }
}