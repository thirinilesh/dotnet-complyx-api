using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class ComplianceFiling
{
    public int FilingId { get; set; }

    public int? CompanyId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Type { get; set; }

    public string? FilingMonth { get; set; }

    public string? FilePath { get; set; }

    public string? Status { get; set; }

    public string? Errors { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Employee? Employee { get; set; }
}
