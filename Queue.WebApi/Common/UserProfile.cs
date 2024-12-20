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
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
            .ForMember(dest=>dest.Login,opt=>opt.MapFrom(src=>src.Login))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash));
    }
}
