using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class Record
{
    public int RecordId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Surname { get; set; }

    public string Iin { get; set; } = null!;

    public int RecordStatusId { get; set; }

    public int ServiceId { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool IsCreatedByEmployee { get; set; }

    public int? CreatedBy { get; set; }

    public int TicketNumber { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ReasonsForCancellation> ReasonsForCancellations { get; set; } = new List<ReasonsForCancellation>();

    public virtual RecordStatus RecordStatus { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Service Service { get; set; } = null!;
}
