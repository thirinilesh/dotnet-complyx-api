using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Tdsreturn
{
    public int ReturnId { get; set; }

    public int DeductorId { get; set; }

    public string FormType { get; set; } = null!;

    public string FinancialYear { get; set; } = null!;

    public string Quarter { get; set; } = null!;

    public string ReturnType { get; set; } = null!;

    public string? OriginalTokenNo { get; set; }

    public string Status { get; set; } = null!;

    public string? Fvuversion { get; set; }

    public string? Rpuversion { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tdsdeductor Deductor { get; set; } = null!;

    public virtual ICollection<TdsreturnChallan> TdsreturnChallans { get; set; } = new List<TdsreturnChallan>();

    public virtual ICollection<TdsreturnEntry> TdsreturnEntries { get; set; } = new List<TdsreturnEntry>();
}
