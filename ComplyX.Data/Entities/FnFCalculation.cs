using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class FnFCalculation
{
    public Guid FnFid { get; set; }

    public DateOnly? ResignationDate { get; set; }

    public DateOnly? LastWorkingDate { get; set; }

    public int? NoticePeriodServedDays { get; set; }

    public decimal? SalaryDue { get; set; }

    public decimal? LeaveEncashmentAmount { get; set; }

    public decimal? GratuityAmount { get; set; }

    public decimal? Bonus { get; set; }

    public decimal? Deductions { get; set; }

    public decimal? NetPayable { get; set; }

    public Guid? ProcessedBy { get; set; }

    public string? PaymentStatus { get; set; }

    public DateOnly? ProcessedDate { get; set; }

    public string? Remarks { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int EmployeeId { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;
}
