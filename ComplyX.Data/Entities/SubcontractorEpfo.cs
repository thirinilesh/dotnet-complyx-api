using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class SubcontractorEpfo
{
    public int SubcontractorEpfoid { get; set; }

    public int SubcontractorId { get; set; }

    public string EstablishmentCode { get; set; } = null!;

    public string? Extension { get; set; }

    public string? OfficeCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Subcontractor Subcontractor { get; set; } = null!;
}
