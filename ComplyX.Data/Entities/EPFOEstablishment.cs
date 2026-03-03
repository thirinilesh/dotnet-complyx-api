using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class EPFOEstablishment
{
    public int CompanyEpfoid { get; set; }

    public int CompanyId { get; set; }

    public string EstablishmentCode { get; set; } = null!;

    public string? Extension { get; set; }

    public string? OfficeCode { get; set; }

    public DateTime? CreatedAt { get; set; }
    public int? SubcontractorId { get; set; }
    public int? BranchId { get; set; }
    public virtual Company Company { get; set; } = null!;
    public virtual Subcontractor? Subcontractor { get; set; }
    public virtual CompanyBranches? CompanyBranches { get; set; }

    public virtual ICollection<Epfoperiod> Epfoperiod { get; set; } = new List<Epfoperiod>();
}
