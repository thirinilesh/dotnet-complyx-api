using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;

public partial class TdsreturnChallan
{
    [Key]
    public int ReturnChallanId { get; set; }

    public int ReturnId { get; set; }

    public int ChallanId { get; set; }

    public virtual Tdschallan Challan { get; set; } = null!;

    public virtual Tdsreturn Return { get; set; } = null!;
}
