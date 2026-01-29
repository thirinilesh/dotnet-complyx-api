using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class PartyMaster
{
    public int PartyId { get; set; }

    public string PartyName { get; set; } = null!;

    public string? Pan { get; set; }

    public string? Gstin { get; set; }

    public string PartyType { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string City { get; set; } = null!;

    public string StateCode { get; set; } = null!;

    public string Pincode { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual ICollection<CompanyPartyRole> CompanyPartyRoles { get; set; } = new List<CompanyPartyRole>();
}
