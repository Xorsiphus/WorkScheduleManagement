using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacationRequestController : ControllerBase
    {

        private readonly ILogger<VacationRequestController> _logger;

        public VacationRequestController(ILogger<VacationRequestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}