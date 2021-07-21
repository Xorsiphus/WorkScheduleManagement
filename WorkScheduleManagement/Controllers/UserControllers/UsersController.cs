using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Application.Models.Users;
using WorkScheduleManagement.Application.CQRS.Queries;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Controllers.UserControllers
{
    [Authorize(Roles = "director,admin,supervisor")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;

        public UsersController(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Index() => 
            View(User.IsInRole("supervisor")
            ? _userManager.GetUsersInRoleAsync("employee").Result.ToList()
            : _userManager.Users.Include(u => u.Position).ToList());

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _mediator.Send(new GetUserById.Query(id));
            if (user == null)
            {
                return NotFound();
            }

            EditUserModel model = new EditUserModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Position = user.Position.Id,
                PhoneNumber = user.PhoneNumber,
                UnusedVacationDaysCount = user.UnusedVacationDaysCount,
                AllPositions = await _mediator.Send(new GetUserPositions.Query())
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            model.AllPositions = await _mediator.Send(new GetUserPositions.Query());

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.FullName = model.FullName;
                    user.UnusedVacationDaysCount = model.UnusedVacationDaysCount;
                    user.Position = await _mediator.Send(new GetPositionById.Query(model.Position));
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
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");
        }
    }
}