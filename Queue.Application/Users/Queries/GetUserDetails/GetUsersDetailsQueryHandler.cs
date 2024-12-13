using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue.Application.Common.Exceptions;
using Queue.Application.Interfaces;
using Queue.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserDetails
{
    public class GetUsersDetailsQueryHandler:
        IRequestHandler<GetUserDetailsQuery,UserDetailsVm>
    {
        private readonly IQueuesDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetUsersDetailsQueryHandler(IQueuesDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        
        public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Users
                .FirstOrDefaultAsync(user=>
                user.Id == request.Id,cancellationToken);

            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(User),request.Id);
            }
            return _mapper.Map<UserDetailsVm>(entity);
        }
    }
}
