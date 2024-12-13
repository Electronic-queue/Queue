using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue.Application.Common.Exceptions;
using Queue.Application.Interfaces;
using Queue.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Queue.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IQueuesDbContext _dbContext;

        public UpdateUserCommandHandler(IQueuesDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            entity.Iin = request.Iin;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
