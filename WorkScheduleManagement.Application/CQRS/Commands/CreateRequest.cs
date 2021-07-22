﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkScheduleManagement.Data.Entities.Requests;
using WorkScheduleManagement.Persistence;

namespace WorkScheduleManagement.Application.CQRS.Commands
{
    public static class CreateRequest
    {
        public record Command(Request Request) : IRequest<bool>;

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly AppDbContext _context;

            public Handler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.Requests.AddAsync(request.Request);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}