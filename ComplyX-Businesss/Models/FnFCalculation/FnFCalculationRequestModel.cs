using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.FnFCalculation
{
    public class FnFCalculationRequestModel
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
    }
}
