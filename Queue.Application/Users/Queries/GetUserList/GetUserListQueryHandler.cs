using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Queue.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHandler:
        IRequestHandler<GetUserListQuery,UserListVm>
    {
        private readonly IQueuesDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserListQueryHandler(IQueuesDbContext dbContext
            ,IMapper mapper)=>(_dbContext,_mapper)=(dbContext,mapper);

        public async Task<UserListVm> Handle(GetUserListQuery request,
            CancellationToken cancellationToken)
        {
            var usersQuery= await _dbContext.Users.
                Where(user=>user.Id==request.Id)
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new UserListVm { Users = usersQuery };
        }
       
    }
}
