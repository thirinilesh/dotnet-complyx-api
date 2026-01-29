using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Epfoecrfile
{
    public int EcrfileId { get; set; }

    public int CompanyId { get; set; }

    public int? SubcontractorId { get; set; }

    public string WageMonth { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public int TotalEmployees { get; set; }

    public decimal TotalWages { get; set; }

    public decimal TotalContribution { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Subcontractor? Subcontractor { get; set; }
}
