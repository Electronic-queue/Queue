using System;
using System.Collections.Generic;

namespace Queue.Domain.Entites;

public partial class Review
{
    public int ReviewId { get; set; }

    public int RecordId { get; set; }

    public int Rating { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual Record Record { get; set; } = null!;
}
