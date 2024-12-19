using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class ReasonsForCancellation
{
    public int ReasonId { get; set; }

    public int RecordId { get; set; }

    public string? Explanation { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Record Record { get; set; } = null!;
}
