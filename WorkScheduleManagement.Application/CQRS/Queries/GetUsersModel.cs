using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkScheduleManagement.Application.Models.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetUsersModel
    {
        public record Query: IRequest<ICollection<UserIdNameModel>>;

        public class Handler : IRequestHandler<Query, ICollection<UserIdNameModel>>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<ICollection<UserIdNameModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _context
                    .Users
                    .Include(u => u.Position)
                    .ToListAsync();
                return users.ConvertAll(u => new UserIdNameModel { Id = u.Id, FullName = u.FullName });
            }
        }
    }
}