using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;

public partial class GratuityPolicy
{
    [Key]
    public Guid PolicyId { get; set; }

    public int MinimumServiceYears { get; set; }

    public string? Formula { get; set; }

    public decimal? MaxGratuityAmount { get; set; }

    public bool Eligible { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;
}
