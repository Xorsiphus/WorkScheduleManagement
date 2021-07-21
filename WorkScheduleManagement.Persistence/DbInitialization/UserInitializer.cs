using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Persistence.DbInitialization
{
    public static class UserInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            var admin = new ApplicationUser
            {
                Email = "test@test.t",
                UserName = "test@test.t",
                FullName = "admin",
                Position = await context.UserPositions.Where(p => p.Name == "position").FirstOrDefaultAsync(),
                PhoneNumber = "3",
                UnusedVacationDaysCount = 20
            };
            const string adminPassword = "11111";

            var director = new ApplicationUser
            {
                Email = "anotherOne@test.t",
                UserName = "anotherOne@test.t",
                FullName = "director",
                Position = await context.UserPositions.Where(p => p.Name == "position2").FirstOrDefaultAsync(),
                PhoneNumber = "4",
                UnusedVacationDaysCount = 20
            };
            const string directorPassword = "11111";

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if (await userManager.FindByNameAsync(director.UserName) == null)
            {
                IdentityResult result = await userManager.CreateAsync(director, directorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(director, "director");
                }
            }
        }
    }
}