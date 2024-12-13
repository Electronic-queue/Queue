using MediatR;
using Queue.Application.Interfaces;
using Queue.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Commands.CreateUser
{
     public class CreateUserCommandHandler
        :IRequestHandler<CreateUserCommand,Guid>
    {
        private readonly IQueuesDbContext _dbContext;

        public CreateUserCommandHandler(IQueuesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle (CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = new User
            {
                Iin=request.Iin,
                FirstName=request.FirstName,
                LastName=request.LastName,
                Id=Guid.NewGuid(),
                CreationDate=DateTime.Now
            };

            await _dbContext.Users.AddAsync(user,cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
