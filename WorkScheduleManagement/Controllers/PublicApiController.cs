using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleManagement.Application.CQRS.Queries;

namespace WorkScheduleManagement.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class PublicApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public PublicApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("User/Positions")]
        public async Task<IActionResult> GetPositions()
        {
            var response = await _mediator.Send(new GetPositions.Query());
            return response == null ? NotFound() : Ok(response);
        }
    }
}