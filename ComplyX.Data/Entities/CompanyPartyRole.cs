using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class CompanyPartyRole
{
    public int CompanyPartyRoleId { get; set; }

    public int CompanyId { get; set; }

    public int PartyId { get; set; }

    public string RoleType { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual PartyMaster Party { get; set; } = null!;
}
