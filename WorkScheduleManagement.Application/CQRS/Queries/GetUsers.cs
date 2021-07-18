using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Data.Entities.Users;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetUsers
    {
        public record Query: IRequest<ICollection<ApplicationUser>>;

        public class Handler : IRequestHandler<Query, ICollection<ApplicationUser>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ICollection<ApplicationUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _context
                    .Users
                    .Include(u => u.Position)
                    .ToListAsync();
                return users;
            }
        }
    }
}