using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class Notification
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

    public virtual User? CreatedByNavigation { get; set; }

    public virtual NotificationType NotificationType { get; set; } = null!;
}
