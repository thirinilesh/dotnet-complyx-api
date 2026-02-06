using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LicenseKeyMaster
{
    public  class LicenseKeyMasterResponseModel
    {
        public int LicenseId { get; set; }

        public int ProductOwnerId { get; set; }

        public string LicenseKey { get; set; } = null!;

        public string PlanType { get; set; } = null!;

        public int MaxActivations { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
