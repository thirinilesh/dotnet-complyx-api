using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class GstHsnMapping
{
    public int MappingId { get; set; }

    public int CompanyId { get; set; }

    public int ItemId { get; set; }

    public string? Hsncode { get; set; }

    public string? Saccode { get; set; }

    public decimal? GstRate { get; set; }
}
