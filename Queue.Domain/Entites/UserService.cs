using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class UserService
{
    public int UserServiceId { get; set; }

    public int UserId { get; set; }

    public int ServiceId { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
