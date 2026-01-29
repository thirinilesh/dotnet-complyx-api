using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class TdsForm16
{
    public int Form16Id { get; set; }

    public int EmployeeId { get; set; }

    public string FinancialYear { get; set; } = null!;

    public string Pdfpath { get; set; } = null!;

    public DateTime? GeneratedOn { get; set; }

    public int? GeneratedBy { get; set; }
}
