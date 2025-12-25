using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class FnF_Calculations
    {
        public int FnFID { get; set; }
        public  int EmployeeID { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public int? NoticePeriodServedDays { get; set; }
        public decimal? SalaryDue { get; set; }
        public decimal? LeaveEncashmentAmount { get; set; }
        public decimal? GratuityAmount { get; set; }
        public decimal? Bonus {  get; set; }
        public decimal? Deductions { get; set; }
        public decimal? NetPayable { get; set; }
        public int? ProcessedBy { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
