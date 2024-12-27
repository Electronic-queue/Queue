using AutoMapper;
using Queue.Application.UserServices.Commands.CreateUserService;
using Queue.Application.UserServices.Commands.UpdateUserService;
using Queue.WebApi.Contracts.UserServiceContracts;

namespace Queue.WebApi.Common.UserServiceProfile;

public class UserServiceProfile:Profile
{
    public UserServiceProfile()
    {
        CreateMap<CreateUserServiceDto, CreateUserServiceCommand>()
            .ForMember(x => x.UserId,
           opt => opt.MapFrom(y => y.UserId))
            .ForMember(x => x.ServiceId,
           opt => opt.MapFrom(y => y.ServiceId))
            .ForMember(x => x.DescriptionRu,
           opt => opt.MapFrom(y => y.DescriptionRu))
            .ForMember(x => x.DescriptionKk,
           opt => opt.MapFrom(y => y.DescriptionKk))
            .ForMember(x => x.DescriptionEn,
           opt => opt.MapFrom(y => y.DescriptionEn))
            .ForMember(x => x.CreatedBy,
           opt => opt.MapFrom(y => y.CreatedBy));
        CreateMap<UpdateUserServiceDto, UpdateUserServiceCommand>()
            .ForMember(x => x.UserServiceId,
           opt => opt.MapFrom(y => y.UserServiceId))
            .ForMember(x => x.UserId,
           opt => opt.MapFrom(y => y.UserId))
            .ForMember(x => x.ServiceId,
           opt => opt.MapFrom(y => y.ServiceId))
            .ForMember(x => x.DescriptionRu,
           opt => opt.MapFrom(y => y.DescriptionRu))
            .ForMember(x => x.DescriptionKk,
           opt => opt.MapFrom(y => y.DescriptionKk))
            .ForMember(x => x.DescriptionEn,
           opt => opt.MapFrom(y => y.DescriptionEn))
            .ForMember(x => x.IsActive,
           opt => opt.MapFrom(y => y.IsActive));
    }
}
