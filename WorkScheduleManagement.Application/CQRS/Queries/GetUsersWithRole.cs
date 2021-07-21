using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WorkScheduleManagement.Application.Models.Requests;
using WorkScheduleManagement.Data.Entities.Users;

namespace WorkScheduleManagement.Application.CQRS.Queries
{
    public static class GetUsersWithRole
    {
        public record Query(string RoleName) : IRequest<ICollection<UserIdNameModel>>;

        public class Handler : IRequestHandler<Query, ICollection<UserIdNameModel>>
        {
            private readonly UserManager<ApplicationUser> _userManager;

            public Handler(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<ICollection<UserIdNameModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var users = await _userManager.GetUsersInRoleAsync(request.RoleName);

                return users.ToList().ConvertAll(u => new UserIdNameModel { Id = u.Id, FullName = u.FullName });
            }
        }
    }
}