using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Tdschallan
{
    public int ChallanId { get; set; }

    public int DeductorId { get; set; }

    public string Bsrcode { get; set; } = null!;

    public DateOnly ChallanDate { get; set; }

    public string ChallanSerialNo { get; set; } = null!;

    public string SectionCode { get; set; } = null!;

    public decimal TaxAmount { get; set; }

    public decimal? InterestAmount { get; set; }

    public decimal? LateFeeAmount { get; set; }

    public decimal? OtherAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public bool MatchedWithOltas { get; set; }

    public bool IsMappedToReturn { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tdsdeductor Deductor { get; set; } = null!;

    public virtual ICollection<TdschallanAllocation> TdschallanAllocations { get; set; } = new List<TdschallanAllocation>();

    public virtual ICollection<TdsreturnChallan> TdsreturnChallans { get; set; } = new List<TdsreturnChallan>();
}
