using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queue.Domain.Entites;

[Table(nameof(ExceedingsTime),Schema = "elec_queue")]
public partial class ExceedingsTime
{
    [Key]
    [Column("exceedings_time_id")]
    public int ExceedingsTimeId { get; set; }
    
    public int WindowId { get; set; }

    public int TimeForExcommunication { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? CanceledOn { get; set; }

    public virtual Window Window { get; set; } = null!;
}
