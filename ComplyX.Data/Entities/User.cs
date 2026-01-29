using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class User
{
    public int UserId { get; set; }

    public int? CompanyId { get; set; }

    public int? SubcontractorId { get; set; }

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public string? Role { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual Company? Company { get; set; }

    public virtual Subcontractor? Subcontractor { get; set; }
}
