using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public  class ComplianceFilings
    {
        [Key]
        public int FilingID { get; set; }
        public int CompanyID { get; set; }
        public int EmployeeID { get; set; }
        public string? Type { get; set; }
        public string? FilingMonth { get; set; }
        public string? FilePath { get; set; }
        public string? Status { get; set; }
        public string? Errors { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
