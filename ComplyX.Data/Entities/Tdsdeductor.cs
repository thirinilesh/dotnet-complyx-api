using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Tdsdeductor
{
    public int DeductorId { get; set; }

    public int CompanyId { get; set; }

    public string DeductorName { get; set; } = null!;

    public string? Tan { get; set; }

    public string? Pan { get; set; }

    public string DeductorCategory { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Aocode { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Tdschallan> Tdschallans { get; set; } = new List<Tdschallan>();

    public virtual ICollection<Tdsentry> Tdsentries { get; set; } = new List<Tdsentry>();

    public virtual ICollection<Tdsreturn> Tdsreturns { get; set; } = new List<Tdsreturn>();
}
