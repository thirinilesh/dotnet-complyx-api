using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.ComplianceDeadline
{
    public class ComplianceDeadlineRequestModel
    {
        public int DeadlineId { get; set; }

        public int CompanyId { get; set; }

        public string ComplianceType { get; set; } = null!;

        public DateOnly PeriodStart { get; set; }

        public DateOnly PeriodEnd { get; set; }

        public DateOnly DueDate { get; set; }

        public string? Status { get; set; }

        public string? AckNumber { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
