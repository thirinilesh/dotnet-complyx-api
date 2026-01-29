using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class TdschallanAllocation
{
    public int AllocationId { get; set; }

    public int ChallanId { get; set; }

    public int EntryId { get; set; }

    public decimal AllocatedTdsamount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Tdschallan Challan { get; set; } = null!;

    public virtual Tdsentry Entry { get; set; } = null!;
}
