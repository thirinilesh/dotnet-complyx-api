using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Plan
{
    public class PlanResponseModel
    {
        public int PlanId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int? MaxEmployees { get; set; }

        public bool? MultiOrg { get; set; }

        public decimal? Price { get; set; }

        public string? BillingCycle { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
