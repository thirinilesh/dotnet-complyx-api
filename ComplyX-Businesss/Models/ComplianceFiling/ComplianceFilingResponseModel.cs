using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.ComplianceFiling
{
    public class ComplianceFilingResponseModel
    {
        public int FilingId { get; set; }

        public int? CompanyId { get; set; }

        public int? EmployeeId { get; set; }

        public string? Type { get; set; }

        public string? FilingMonth { get; set; }

        public string? FilePath { get; set; }

        public string? Status { get; set; }

        public string? Errors { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
