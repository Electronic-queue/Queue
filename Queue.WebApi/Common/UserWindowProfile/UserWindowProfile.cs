using AutoMapper;
using Queue.Application.UserWindows.Commands.CreateUserWindow;
using Queue.Application.UserWindows.Commands.UpdateUserWindow;
using Queue.WebApi.Contracts.UserWindowComtracts;

namespace Queue.WebApi.Common.UserWindowProfile;

public class UserWindowProfile:Profile
{
    public UserWindowProfile()
    {
        CreateMap<CreateUserWindowDto, CreateUserWindowCommand>()
            .ForMember(x => x.UserId,
           opt => opt.MapFrom(y => y.UserId))
            .ForMember(x => x.WindowId,
           opt => opt.MapFrom(y => y.WindowId))
            .ForMember(x => x.CreatedBy,
           opt => opt.MapFrom(y => y.CreatedBy));
        CreateMap<UpdateUserWindowDto, UpdateUserWindowCommand>()
            .ForMember(x => x.UserWindowId,
           opt => opt.MapFrom(y => y.UserWindowId))
            .ForMember(x => x.UserId,
           opt => opt.MapFrom(y => y.UserId))
            .ForMember(x => x.WindowId,
           opt => opt.MapFrom(y => y.WindowId));
    }
}
