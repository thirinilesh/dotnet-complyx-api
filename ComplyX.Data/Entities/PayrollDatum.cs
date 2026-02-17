using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class PayrollDatum
{
    public int PayrollId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Month { get; set; }

    public decimal? Basic { get; set; }

    public decimal? Hra { get; set; }

    public decimal? SpecialAllowance { get; set; }

    public decimal? VariablePay { get; set; }

    public decimal? GrossSalary { get; set; }

    public decimal? Pf { get; set; }

    public decimal? Esi { get; set; }

    public decimal? ProfessionalTax { get; set; }

    public decimal? Tds { get; set; }

    public decimal? NetPay { get; set; }

    public string? BankAccount { get; set; }

    public string? Ifsc { get; set; }
    public string? TaxYear { get; set; }

    public virtual Employee? Employee { get; set; }
}
