using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class Window
{
    public int WindowId { get; set; }

    public int WindowStatusId { get; set; }

    public int WindowNumber { get; set; }

    public DateTime CreatedOn { get; set; }
    

    public int? CreatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<ExceedingsTime> ExceedingsTimes { get; set; } = new List<ExceedingsTime>();

    public virtual ICollection<UserWindow> UserWindows { get; set; } = new List<UserWindow>();

    public virtual WindowStatus WindowStatus { get; set; } = null!;
}
