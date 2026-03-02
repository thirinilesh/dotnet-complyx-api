using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComplyX.Data.Entities;

public partial class TdsreturnEntry
{
    [Key]
    public int ReturnEntryId { get; set; }

    public int ReturnId { get; set; }

    public int EntryId { get; set; }

    public virtual Tdsentry Entry { get; set; } = null!;

    public virtual Tdsreturn Return { get; set; } = null!;
}
