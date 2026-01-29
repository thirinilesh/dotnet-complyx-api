using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Tdsentry
{
    public int EntryId { get; set; }

    public int DeductorId { get; set; }

    public int DeducteeId { get; set; }

    public string SectionCode { get; set; } = null!;

    public DateOnly PaymentDate { get; set; }

    public decimal AmountPaid { get; set; }

    public decimal TaxableAmount { get; set; }

    public decimal Tdsrate { get; set; }

    public decimal Tdsamount { get; set; }

    public decimal? Surcharge { get; set; }

    public decimal? Cess { get; set; }

    public decimal? TotalTds { get; set; }

    public bool HigherRateApplied { get; set; }

    public string? HigherRateReason { get; set; }

    public bool IsMappedToReturn { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tdsdeductee Deductee { get; set; } = null!;

    public virtual Tdsdeductor Deductor { get; set; } = null!;

    public virtual TdschallanAllocation? TdschallanAllocation { get; set; }

    public virtual ICollection<TdsreturnEntry> TdsreturnEntries { get; set; } = new List<TdsreturnEntry>();
}
