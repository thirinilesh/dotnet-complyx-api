using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class SubscriptionPlan
{
    public int PlanId { get; set; }

    public string PlanCode { get; set; } = null!;

    public string PlanName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? PriceMonthly { get; set; }

    public decimal? PriceYearly { get; set; }

    public int MaxCompanies { get; set; }

    public int MaxUsers { get; set; }

    public int MaxStorageMb { get; set; }

    public bool? AllowEpfo { get; set; }

    public bool? AllowEsic { get; set; }

    public bool? AllowGst { get; set; }

    public bool? AllowTds { get; set; }

    public bool? AllowClra { get; set; }

    public bool? AllowLwf { get; set; }

    public bool? AllowPt { get; set; }

    public bool? AllowPayroll { get; set; }

    public bool? AllowDscsigning { get; set; }

    public bool? AllowCloudBackup { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<ProductOwnerSubscription> ProductOwnerSubscriptions { get; set; } = new List<ProductOwnerSubscription>();
}
