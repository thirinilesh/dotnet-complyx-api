using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Tdsdeductee
{
    public int DeducteeId { get; set; }

    public int CompanyId { get; set; }

    public string DeducteeType { get; set; } = null!;

    public string DeducteeName { get; set; } = null!;

    public string? Pan { get; set; }

    public string Panstatus { get; set; } = null!;

    public bool? AadhaarLinked { get; set; }

    public string ResidentStatus { get; set; } = null!;

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Tdsentry> Tdsentries { get; set; } = new List<Tdsentry>();
}
