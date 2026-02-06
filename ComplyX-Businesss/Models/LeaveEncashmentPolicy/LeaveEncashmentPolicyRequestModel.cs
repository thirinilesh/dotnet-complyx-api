using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LeaveEncashmentPolicy
{
    public class LeaveEncashmentPolicyRequestModel
    {
        public Guid PolicyId { get; set; }

        public string LeaveType { get; set; } = null!;

        public bool EncashmentAllowed { get; set; }

        public int? MaxEncashableDays { get; set; }

        public string? EncashmentFrequency { get; set; }

        public string? EncashmentFormula { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int CompanyId { get; set; }
    }
}
