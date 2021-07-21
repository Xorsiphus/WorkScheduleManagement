using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleManagement.Application.CQRS.Queries;

namespace WorkScheduleManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index() =>
            View(await _mediator.Send(new GetUserById.Query(User.FindFirstValue(ClaimTypes.NameIdentifier))));
    }
}