using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.SubscriptionPlan
{
    public class SubscriptionPlanRequestModel
    {
        public int PlanId { get; set; }

        public string PlanCode { get; set; } = null!;

        public string PlanName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal? PriceMonthly { get; set; }

        public decimal? PriceYearly { get; set; }

        public int MaxCompanies { get; set; }

        public int MaxUsers { get; set; }

        public int MaxStorageMb { get; set; }

        public bool? AllowEpfo { get; set; }

        public bool? AllowEsic { get; set; }

        public bool? AllowGst { get; set; }

        public bool? AllowTds { get; set; }

        public bool? AllowClra { get; set; }

        public bool? AllowLwf { get; set; }

        public bool? AllowPt { get; set; }

        public bool? AllowPayroll { get; set; }

        public bool? AllowDscsigning { get; set; }

        public bool? AllowCloudBackup { get; set; }

        public bool? IsActive { get; set; }
    }
}
