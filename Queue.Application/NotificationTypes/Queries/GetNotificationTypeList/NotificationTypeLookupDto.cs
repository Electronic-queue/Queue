using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;
using Queue.Domain.Entites;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;

public class NotificationTypeLookupDto : IMapWith<NotificationType>
{
    public int NotificationTypeId { get; set; }

    public string NameRu { get; set; } = null!;

    public string NameKk { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<NotificationType, NotificationTypeDetailsVm>()
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
            opt => opt.MapFrom(notificationType => notificationType.DescriptionEn))
            .ForMember(notificationTypeVm => notificationTypeVm.CreatedOn,
            opt => opt.MapFrom(notificationType => notificationType.CreatedOn))
            .ForMember(notificationTypeVm => notificationTypeVm.CreatedBy,
            opt => opt.MapFrom(notificationType => notificationType.CreatedBy));

    }
}
