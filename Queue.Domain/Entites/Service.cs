using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class Service
{
    public int ServiceId { get; set; }

    public string NameRu { get; set; } = null!;

    public string NameKk { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public int AverageExecutionTime { get; set; }

    public int? ParentserviceId { get; set; }

    public int QueueTypeId { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<Service> InverseParentservice { get; set; } = new List<Service>();

    public virtual Service? Parentservice { get; set; }

    public virtual QueueType QueueType { get; set; } = null!;

    public virtual ICollection<Record> Records { get; set; } = new List<Record>();

    public virtual ICollection<UserService> UserServices { get; set; } = new List<UserService>();
}
