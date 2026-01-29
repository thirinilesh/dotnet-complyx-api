using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class TdsForm16A
{
    public int Form16Aid { get; set; }

    public int VendorId { get; set; }

    public string FinancialYear { get; set; } = null!;

    public string Quarter { get; set; } = null!;

    public string Pdfpath { get; set; } = null!;

    public DateTime? GeneratedOn { get; set; }

    public int? GeneratedBy { get; set; }
}
