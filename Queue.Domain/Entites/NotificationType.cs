 using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class NotificationType
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

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
