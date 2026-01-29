using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class TdsCalculation
{
    public int TdscalcId { get; set; }

    public int EmployeeId { get; set; }

    public string Pan { get; set; } = null!;

    public string FinancialYear { get; set; } = null!;

    public string Tdssection { get; set; } = null!;

    public decimal IncomeAmount { get; set; }

    public decimal? ExemptionAmount { get; set; }

    public decimal? TaxableAmount { get; set; }

    public decimal Tdsrate { get; set; }

    public decimal Tdsamount { get; set; }

    public bool IsSalary { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }
}
