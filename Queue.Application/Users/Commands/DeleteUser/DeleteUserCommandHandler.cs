using MediatR;
using Queue.Application.Common.Exceptions;
using Queue.Application.Interfaces;
using Queue.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler :
        IRequestHandler<DeleteUserCommand>
    {
        public readonly IQueuesDbContext _dbContext;

        public DeleteUserCommandHandler(IQueuesDbContext dbContext) =>
            _dbContext = dbContext;
        public async Task<Unit> Handle(DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(User),request.Id);
            }
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
