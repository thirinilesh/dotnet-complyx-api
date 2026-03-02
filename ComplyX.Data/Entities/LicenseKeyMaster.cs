using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;

public partial class LicenseKeyMaster
{
    [Key]
    public int LicenseId { get; set; }

    public int ProductOwnerId { get; set; }

    public string LicenseKey { get; set; } = null!;

    public string PlanType { get; set; } = null!;

    public int MaxActivations { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<LicenseActivation> LicenseActivations { get; set; } = new List<LicenseActivation>();

    public virtual ProductOwner ProductOwner { get; set; } = null!;
}
