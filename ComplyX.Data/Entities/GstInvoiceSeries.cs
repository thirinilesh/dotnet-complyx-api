using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class GstInvoiceSeries
{
    public int SeriesId { get; set; }

    public int CompanyId { get; set; }

    public string FinancialYear { get; set; } = null!;

    public string? Prefix { get; set; }

    public int? CurrentNumber { get; set; }

    public string? Suffix { get; set; }

    public DateTime? LastUpdated { get; set; }
}
