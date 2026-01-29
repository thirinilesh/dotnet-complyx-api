using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class LicenseActivation
{
    public int ActivationId { get; set; }

    public int LicenseId { get; set; }

    public string MachineHash { get; set; } = null!;

    public string? MachineName { get; set; }

    public string? OsUser { get; set; }

    public DateTime ActivatedAt { get; set; }

    public DateTime? LastVerifiedAt { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime? GraceExpiryAt { get; set; }

    public string? AppVersion { get; set; }

    public virtual LicenseKeyMaster License { get; set; } = null!;

    public virtual ICollection<MachineBinding> MachineBindings { get; set; } = new List<MachineBinding>();
}
