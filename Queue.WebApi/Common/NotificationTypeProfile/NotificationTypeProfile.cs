using AutoMapper;
using Queue.Application.NotificationTypes.Commands.CreateNotificationType;
using Queue.Application.NotificationTypes.Commands.UpdateNotificationType;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.NotificationTypeContracts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Queue.WebApi.Common.NotificationTypeProfile;

public class NotificationTypeProfile : Profile
{
    public NotificationTypeProfile()
    {
        CreateMap<CreateNotificationTypeDto, CreateNotificationTypeCommand>()
                .ForMember(notificationTypeVm => notificationTypeVm.NameRu,
                opt => opt.MapFrom(notificationType => notificationType.NameRu))
                .ForMember(notificationTypeVm => notificationTypeVm.NameKk,
                opt => opt.MapFrom(notificationType => notificationType.NameKk))
                .ForMember(notificationTypeVm => notificationTypeVm.NameEn,
                opt => opt.MapFrom(notificationType => notificationType.NameEn))
                .ForMember(notificationTypeVm => notificationTypeVm.DescriptionRu,
                opt => opt.MapFrom(notificationType => notificationType.DescriptionRu))
                .ForMember(notificationTypeVm => notificationTypeVm.DescriptionKk,
                opt => opt.MapFrom(notificationType => notificationType.DescriptionKk))
                .ForMember(notificationTypeVm => notificationTypeVm.DescriptionEn,
                opt => opt.MapFrom(notificationType => notificationType.DescriptionEn))
                .ForMember(notificationTypeVm => notificationTypeVm.CreatedBy,
                opt => opt.MapFrom(notificationType => notificationType.CreatedBy));

        CreateMap<UpdateNotificationTypeDto, UpdateNotificationTypeCommand>()
                .ForMember(notificationTypeVm => notificationTypeVm.NotificationTypeId,
                opt => opt.MapFrom(notificationType => notificationType.NotificationTypeId))
                .ForMember(notificationTypeVm => notificationTypeVm.NameRu,
                opt => opt.MapFrom(notificationType => notificationType.NameRu))
                .ForMember(notificationTypeVm => notificationTypeVm.NameKk,
                opt => opt.MapFrom(notificationType => notificationType.NameKk))
                .ForMember(notificationTypeVm => notificationTypeVm.NameEn,
                opt => opt.MapFrom(notificationType => notificationType.NameEn))
                .ForMember(notificationTypeVm => notificationTypeVm.DescriptionRu,
                opt => opt.MapFrom(notificationType => notificationType.DescriptionRu))
                .ForMember(notificationTypeVm => notificationTypeVm.DescriptionKk,
                opt => opt.MapFrom(notificationType => notificationType.DescriptionKk))
                .ForMember(notificationTypeVm => notificationTypeVm.DescriptionEn,
                opt => opt.MapFrom(notificationType => notificationType.DescriptionEn));
    }
}
