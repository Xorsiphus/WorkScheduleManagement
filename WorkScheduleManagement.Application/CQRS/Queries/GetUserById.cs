using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Users;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetUserById
    {
        public record Query(string Id) : IRequest<ApplicationUser>;

        public class Handler : IRequestHandler<Query, ApplicationUser>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ApplicationUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context
                    .Users
                    .Where(u => u.Id == request.Id)
                    .Include(u => u.Position)
                    .FirstOrDefaultAsync();
                return user == null ? null : user;
            }
        }
    }
}