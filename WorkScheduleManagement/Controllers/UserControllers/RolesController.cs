using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkScheduleManagement.Application.Models.Users;
using WorkScheduleManagement.Data.Entities.Users;


namespace WorkScheduleManagement.Controllers.UserControllers
{
    [Authorize(Roles = "director")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        // private readonly SignInManager<ApplicationUser> _singInManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
            // SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            // _singInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index() => View(_userManager.Users.ToList());

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeUserRoleModel model = new ChangeUserRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles).ToList();
                var removedRoles = userRoles.Except(roles).ToList();

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}