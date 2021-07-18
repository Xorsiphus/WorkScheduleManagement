using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WorkScheduleManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}