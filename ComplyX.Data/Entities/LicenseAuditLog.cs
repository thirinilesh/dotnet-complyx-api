using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class LicenseAuditLog
{
    public int AuditId { get; set; }

    public int LicenseId { get; set; }

    public int? ActivationId { get; set; }

    public string? EventType { get; set; }

    public string? EventMessage { get; set; }

    public DateTime LoggedAt { get; set; }

    public string? MachineHash { get; set; }

    public string? Ipaddress { get; set; }
}
