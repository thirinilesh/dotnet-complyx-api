using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComplyX.Data.Entities;

public partial class TdschallanAllocation
{
    [Key]
    [ForeignKey("Entry")]
    public int AllocationId { get; set; }

    public int ChallanId { get; set; }

    public int EntryId { get; set; }

    public decimal AllocatedTdsamount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Tdschallan Challan { get; set; } = null!;

    public virtual Tdsentry Entry { get; set; } = null!;
}
