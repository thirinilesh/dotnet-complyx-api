using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class FnF_Calculations
    {
        [Key]
        public Guid FnFID { get; set; }
        public  int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public DateTime? ResignationDate { get; set; }
        public DateTime? LastWorkingDate { get; set; }
        public int? NoticePeriodServedDays { get; set; }
        public decimal? SalaryDue { get; set; }
        public decimal? LeaveEncashmentAmount { get; set; }
        public decimal? GratuityAmount { get; set; }
        public decimal? Bonus {  get; set; }
        public decimal? Deductions { get; set; }
        public decimal? NetPayable { get; set; }
        public Guid? ProcessedBy { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string? Remarks { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
    }
}
