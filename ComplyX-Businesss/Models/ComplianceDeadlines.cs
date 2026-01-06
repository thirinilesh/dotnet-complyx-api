using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class ComplianceDeadlines
    {
        [Key]
        public int DeadlineID { get; set; }
        public int CompanyID { get; set; }
        public string ComplianceType { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public  DateTime DueDate { get; set; }
        public string? Status { get; set; }
        public string?  AckNumber {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
