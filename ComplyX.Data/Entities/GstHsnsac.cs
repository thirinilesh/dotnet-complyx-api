using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class GstHsnsac
{
    public int CodeId { get; set; }

    public string CodeType { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public decimal GstRate { get; set; }
}
