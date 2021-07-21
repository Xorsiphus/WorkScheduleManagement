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
using WorkScheduleManagement.Data.Entities.Requests;
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
        public async Task<IActionResult> Create() => View(new RequestCreationModel
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
        public async Task<IActionResult> Create(RequestCreationModel model)
        {
            model.AllTypes = await _mediator.Send(new GetRequestTypes.Query());
            model.AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query());
            model.AllReplacerUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Union(await _mediator.Send(new GetUsersWithRole.Query("employee")))
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}});
            model.AllApproverUsers = _mediator.Send(new GetUsersWithRole.Query("supervisor")).Result
                .Concat(new[] {new UserIdNameModel {Id = null, FullName = "Не выбран"}});

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
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),
                            DateFrom = model.DateFrom,
                            DateTo = model.DateTo,
                            
                        };
                        break;
                    case RequestType.OnRemoteWork:
                        newRequest = new RemoteWorkRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),
                            
                        };
                        break;
                    case RequestType.OnDayOffInsteadOverworking:
                        newRequest = new DayOffInsteadOverworkingRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),
                            
                        };
                        break;
                    case RequestType.OnDayOffInsteadVacation:
                        newRequest = new DayOffInsteadVacationRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),

                        };
                        break;
                    case RequestType.OnVacation:
                        newRequest = new VacationRequest
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
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

            return View(model);
        }
    }
}