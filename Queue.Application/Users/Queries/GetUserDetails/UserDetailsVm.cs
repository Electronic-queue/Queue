using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Users.Queries.GetUserDetails
{
    public class UserDetailsVm:IMapWith<User>
    {
        public Guid Id { get; set; }
        public int Iin {  get; set; }
        public string FirstName { get; set;}

        public string LastName { get; set;}
        public DateTime CreationDate {  get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>()
                .ForMember(userVm => userVm.Iin,
                opt => opt.MapFrom(user => user.Iin))
                .ForMember(userVm => userVm.FirstName,
                opt => opt.MapFrom(user => user.FirstName))
                .ForMember(userVm => userVm.LastName,
                opt => opt.MapFrom(user => user.LastName))
                .ForMember(userVm => userVm.Id,
                opt => opt.MapFrom(user => user.Id))
                .ForMember(userVm => userVm.CreationDate,
                opt => opt.MapFrom(user => user.CreationDate));

        }
    }
}
