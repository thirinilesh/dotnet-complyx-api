using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class CompanySubcontractor
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public int? SubcontractorId { get; set; }

    public string? RelationType { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Subcontractor? Subcontractor { get; set; }
}
