using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class ComplianceSchedule
{
    public int ScheduleId { get; set; }

    public int CompanyId { get; set; }

    public string ComplianceType { get; set; } = null!;

    public string Frequency { get; set; } = null!;

    public string? StateCode { get; set; }

    public int? BaseDay { get; set; }

    public int? QuarterMonth { get; set; }

    public int? OffsetDays { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
