using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class Tdsrate
{
    public int TaxId { get; set; }

    public string TaxName { get; set; } = null!;

    public decimal Rate { get; set; }

    public string TaxType { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
