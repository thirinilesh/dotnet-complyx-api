using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;

public partial class LeaveEncashmentPolicy
{
    [Key]
    public Guid PolicyId { get; set; }

    public string LeaveType { get; set; } = null!;

    public bool EncashmentAllowed { get; set; }

    public int? MaxEncashableDays { get; set; }

    public string? EncashmentFrequency { get; set; }

    public string? EncashmentFormula { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;
}
