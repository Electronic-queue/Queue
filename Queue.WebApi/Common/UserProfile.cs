using AutoMapper;
using Queue.Application.Users.Commands.CreateUser;
using Queue.WebApi.Models;

namespace Queue.WebApi.Common;

public class UserProfile : Profile
{ 
    public UserProfile()
    {
        CreateMap<CreateUserDto, CreateUserCommand>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
    }
}
