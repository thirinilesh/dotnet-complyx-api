using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.GratuityPolicy
{
    public class GratuityPolicyResponseModel
    {
        public Guid PolicyId { get; set; }

        public int MinimumServiceYears { get; set; }

        public string? Formula { get; set; }

        public decimal? MaxGratuityAmount { get; set; }

        public bool Eligible { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int CompanyId { get; set; }
    }
}
