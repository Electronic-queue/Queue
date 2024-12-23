using AutoMapper;
using Queue.Application.Notifications.Commands.CreateNotification;
using Queue.Application.Notifications.Commands.UpdateNotification;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.NotificationContracts;

namespace Queue.WebApi.Common.NotificationProfile;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<CreateNotificationDto, CreateNotificationCommand>()
                .ForMember(notificationVm => notificationVm.NameRu,
                opt => opt.MapFrom(notification => notification.NameRu))
                .ForMember(notificationVm => notificationVm.NameKk,
                opt => opt.MapFrom(notification => notification.NameKk))
                .ForMember(notificationVm => notificationVm.NameEn,
                opt => opt.MapFrom(notification => notification.NameEn))
                .ForMember(notificationVm => notificationVm.ContentRu,
                opt => opt.MapFrom(notification => notification.ContentRu))
                .ForMember(notificationVm => notificationVm.ContentKk,
                opt => opt.MapFrom(notification => notification.ContentKk))
                .ForMember(notificationVm => notificationVm.ContentEn,
                opt => opt.MapFrom(notification => notification.ContentEn))
                .ForMember(notificationVm => notificationVm.NotificationTypeId,
                opt => opt.MapFrom(notification => notification.NotificationTypeId))
                .ForMember(notificationVm => notificationVm.CreatedBy,
                opt => opt.MapFrom(notification => notification.CreatedBy));

        CreateMap<UpdateNotificationDto, UpdateNotificationCommand>()
                .ForMember(notificationVm => notificationVm.NotificationId,
                opt => opt.MapFrom(notification => notification.NotificationId))
                .ForMember(notificationVm => notificationVm.NameRu,
                opt => opt.MapFrom(notification => notification.NameRu))
                .ForMember(notificationVm => notificationVm.NameKk,
                opt => opt.MapFrom(notification => notification.NameKk))
                .ForMember(notificationVm => notificationVm.NameEn,
                opt => opt.MapFrom(notification => notification.NameEn))
                .ForMember(notificationVm => notificationVm.ContentRu,
                opt => opt.MapFrom(notification => notification.ContentRu))
                .ForMember(notificationVm => notificationVm.ContentKk,
                opt => opt.MapFrom(notification => notification.ContentKk))
                .ForMember(notificationVm => notificationVm.ContentEn,
                opt => opt.MapFrom(notification => notification.ContentEn))
                .ForMember(notificationVm => notificationVm.NotificationTypeId,
                opt => opt.MapFrom(notification => notification.NotificationTypeId));
    }
}
