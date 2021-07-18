using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Persistence
{
    public static class UserPositionsInitializer
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            var positions = new List<UserPosition>
            {
                new UserPosition { Name = "test", NumberOfWorkingHours = 20},
                new UserPosition { Name = "test2", NumberOfWorkingHours = 30},
                new UserPosition { Name = "test3", NumberOfWorkingHours = 40}
            };

            foreach (var position in positions)
            {
                if (await context.UserPositions.Where(p => p.Name == position.Name).FirstOrDefaultAsync() == null)
                {
                    await context.UserPositions.AddAsync(position);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}