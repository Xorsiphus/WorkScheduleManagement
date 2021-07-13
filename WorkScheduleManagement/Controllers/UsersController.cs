using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleManagement.Application.Models.Users;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
 
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
 
        [HttpGet]
        public IActionResult Index() => View(_userManager.Users.ToList());
 
        [HttpGet]
        public IActionResult Create() => View();
 
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Email = model.Email, 
                    UserName = model.Email, 
                    FullName = model.FullName,
                    Position = model.Position,
                    PhoneNumber = model.PhoneNumber,
                    UnusedVacationDaysCount = 20
                };
                
                // TODO Привязка роли к пользователю
                
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
 
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserModel model = new EditUserModel
            {
                Id = user.Id, 
                Email = user.Email, 
                FullName = user.FullName,
                Position = user.Position,
                PhoneNumber = user.PhoneNumber
            };
            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if(user!=null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FullName = model.FullName;
                    user.Position = model.Position;
                    user.PhoneNumber = model.PhoneNumber;
                     
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}