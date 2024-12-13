using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Application.Users.Queries.GetUserDetails;
using Queue.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserList
{
    public class UserLookupDto:IMapWith<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                 .ForMember(userVm => userVm.FirstName,
                opt => opt.MapFrom(user => user.FirstName))
                .ForMember(userVm => userVm.LastName,
                opt => opt.MapFrom(user => user.LastName))
                .ForMember(userVm => userVm.Id,
                opt => opt.MapFrom(user => user.Id));
                

        }
    }
}
