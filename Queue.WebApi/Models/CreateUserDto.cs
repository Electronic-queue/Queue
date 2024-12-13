using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Application.Users.Commands.CreateUser;

namespace Queue.WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public int Iin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCommand => userCommand.FirstName,
                    opt => opt.MapFrom(userDto => userDto.FirstName))
                .ForMember(userCommand => userCommand.LastName,
                    opt => opt.MapFrom(userDto => userDto.LastName))
                .ForMember(userCommand => userCommand.Iin,
                    opt => opt.MapFrom(userDto => userDto.Iin));
        }
    }
}
