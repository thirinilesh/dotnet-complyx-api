using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class Leave_Encashment_Policy
    {
        public Guid PolicyID { get; set; }
        public int CompanyID { get; set; }
        public string LeaveType { get; set; }
        public bool EncashmentAllowed { get; set; }
        public int? MaxEncashableDays { get; set; }
        public string? EncashmentFrequency { get; set; }
        public string? EncashmentFormula { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Company? Companies { get; set; }
    }
}
