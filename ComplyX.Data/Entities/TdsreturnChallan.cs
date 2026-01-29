using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class TdsreturnChallan
{
    public int ReturnChallanId { get; set; }

    public int ReturnId { get; set; }

    public int ChallanId { get; set; }

    public virtual Tdschallan Challan { get; set; } = null!;

    public virtual Tdsreturn Return { get; set; } = null!;
}
