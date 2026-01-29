using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class CompanyEpfo
{
    public int CompanyEpfoid { get; set; }

    public int CompanyId { get; set; }

    public string EstablishmentCode { get; set; } = null!;

    public string? Extension { get; set; }

    public string? OfficeCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;
}
