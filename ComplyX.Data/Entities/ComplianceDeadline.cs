using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class ComplianceDeadline
{
    public int DeadlineId { get; set; }

    public int CompanyId { get; set; }

    public string ComplianceType { get; set; } = null!;

    public DateOnly PeriodStart { get; set; }

    public DateOnly PeriodEnd { get; set; }

    public DateOnly DueDate { get; set; }

    public string? Status { get; set; }

    public string? AckNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
