using Microsoft.AspNetCore.Mvc;

namespace WorkScheduleManagement.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet]
        public IActionResult Error403() => View();
        
        [HttpGet]
        public IActionResult Error404() => View();
    }
}