using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class FilingStatus
{
    public int FilingStatusId { get; set; }

    public string Name { get; set; } = null!;
}
