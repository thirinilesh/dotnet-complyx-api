using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class MachineBinding
{
    public int MachineBindingId { get; set; }

    public string MachineHash { get; set; } = null!;

    public string? Cpuid { get; set; }

    public string? MotherboardSerial { get; set; }

    public string? Macaddresses { get; set; }

    public string? WindowsSid { get; set; }

    public DateTime FirstSeenAt { get; set; }

    public DateTime? LastSeenAt { get; set; }

    public int LicenseActivationId { get; set; }

    public virtual LicenseActivation LicenseActivation { get; set; } = null!;
}
