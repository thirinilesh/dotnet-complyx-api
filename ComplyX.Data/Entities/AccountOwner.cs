using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class AccountOwner
{
    public int AccountOwnerId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string LegalName { get; set; } = null!;

    public string RegistrationId { get; set; } = null!;

    public string OrganizationType { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}
