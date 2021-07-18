using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleManagement.Application.CQRS.Commands;
using WorkScheduleManagement.Application.CQRS.Queries;
using WorkScheduleManagement.Application.Models.Requests;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Data.Entities.Users;
using WorkScheduleManagement.Data.Enums;

namespace WorkScheduleManagement.Controllers
{
    public class RequestsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public RequestsController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index() => View();

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create() => View(new RequestCreationModel
        {
            AllTypes = await _mediator.Send(new GetRequestTypes.Query()),
            AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query()),
            AllUsers = await _mediator.Send(new GetUsersModel.Query()),
            DateFrom = DateTime.Today,
            DateTo = DateTime.Today
        });
        
        [HttpPost]
        public async Task<IActionResult> Create(RequestCreationModel model)
        {
            model.AllTypes = await _mediator.Send(new GetRequestTypes.Query());
            model.AllVacationTypes = await _mediator.Send(new GetRequestVacationTypes.Query());
            model.AllUsers = await _mediator.Send(new GetUsersModel.Query());
            
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
                            
                            
                        };
                        break;
                    case RequestType.OnVacation:
                        newRequest = new VacationRequest()
                        {
                            Creator = await _mediator.Send(new GetUserById.Query(userId)),
                            CreatedAt = DateTime.Now,
                            RequestTypes = await _mediator.Send(new GetRequestTypeById.Query(model.Type)),
                            Comment = model.Comment,
                            
                            DateFrom = model.DateFrom,
                            DateTo = model.DateTo,
                            RequestStatus = await _mediator.Send(new GetRequestStatusById.Query(RequestStatus.New)),
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
            }

            return View(model);
        }
    }
}