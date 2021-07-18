using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Persistence
{
    public static class UserInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser
            {
                Email = "test@test.t",
                UserName = "test",
                FullName = "",
                Position = null,
                PhoneNumber = "",
                UnusedVacationDaysCount = 20
            };
            const string adminPassword = "11111";

            var director = new ApplicationUser
            {
                Email = "anotherOne@test.t",
                UserName = "test2",
                FullName = "",
                Position = null,
                PhoneNumber = "",
                UnusedVacationDaysCount = 20
            };
            const string directorPassword = "11111";

            if (await userManager.FindByNameAsync(admin.Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if (await userManager.FindByNameAsync(director.Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(director, directorPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "director");
                }
            }
        }
    }
}