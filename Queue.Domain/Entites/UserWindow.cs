using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class UserWindow
{
    public int UserWindowId { get; set; }

    public int UserId { get; set; }

    public int WindowId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateTime CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Window Window { get; set; } = null!;
}
