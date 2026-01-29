using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class ExitType
{
    public int ExitTypeId { get; set; }

    public string Name { get; set; } = null!;
}
