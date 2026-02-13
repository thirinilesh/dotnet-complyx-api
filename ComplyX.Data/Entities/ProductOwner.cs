using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class ProductOwner
{
    public int ProductOwnerId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Mobile { get; set; }

    public string? OrganizationName { get; set; }

    public string? LegalName { get; set; }

    public string? RegistrationId { get; set; }

    public string? OrganizationType { get; set; }

    public string? City { get; set; }

    public string? Pincode { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public string SubscriptionPlan { get; set; } = null!;

    public DateOnly SubscriptionStart { get; set; }

    public DateOnly SubscriptionExpiry { get; set; }

    public int MaxCompanies { get; set; }

    public int MaxUsers { get; set; }

    public int MaxStorageMb { get; set; }

    public bool? AllowCloudBackup { get; set; }

    public bool? AllowGstmodule { get; set; }

    public bool? AllowTdsmodule { get; set; }

    public bool? AllowClramodule { get; set; }

    public bool? AllowPayrollModule { get; set; }

    public bool? AllowDscsigning { get; set; }
    public string? BusinessCategory { get; set; }

    public string? BusinessAddress { get; set; }

    public bool? IsDiffPaymentAddress { get; set; }

    public string? PaymentAddress { get; set; }


    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<LicenseKeyMaster> LicenseKeyMasters { get; set; } = new List<LicenseKeyMaster>();

    public virtual ICollection<ProductOwnerSubscription> ProductOwnerSubscriptions { get; set; } = new List<ProductOwnerSubscription>();
}
