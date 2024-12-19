using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class ExceedingsTime
{
    public int ExceedingsTimeId { get; set; }

    public int WindowId { get; set; }

    public int TimeForExcommunication { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? CanceledOn { get; set; }

    public virtual Window Window { get; set; } = null!;
}
