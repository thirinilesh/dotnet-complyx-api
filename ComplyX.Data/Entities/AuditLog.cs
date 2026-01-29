using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class AuditLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string? Action { get; set; }

    public string? TargetType { get; set; }

    public int? TargetId { get; set; }

    public string? Details { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual User? User { get; set; }
}
