using AutoMapper;
using Queue.Application.Common.Mappings;
using Queue.Application.Notifications.Queries.GetNotificationById;
using Queue.Application.Users.Queries.GetUserDetails;
using Queue.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Notifications.Queries.GetNotificationList
{
    public class NotificationLookupDto : IMapWith<Notification>
    {
        public int NotificationId { get; set; }

        public string NameRu { get; set; } = null!;

        public string NameKk { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string? ContentRu { get; set; }

        public string? ContentKk { get; set; }

        public string? ContentEn { get; set; }

        public int NotificationTypeId { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationByIdVm>()
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
                opt => opt.MapFrom(notification => notification.NotificationTypeId))
                .ForMember(notificationVm => notificationVm.CreatedOn,
                opt => opt.MapFrom(notification => notification.CreatedOn))
                .ForMember(notificationVm => notificationVm.CreatedBy,
                opt => opt.MapFrom(notification => notification.CreatedBy));

        }
    }
}
