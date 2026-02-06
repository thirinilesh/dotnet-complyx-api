using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Company
{
    public class CompanyRequestModel
    {
        public int CompanyId { get; set; }

        public string Name { get; set; } = null!;

        public string? Domain { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? Address { get; set; }

        public string? State { get; set; }

        public string? Gstin { get; set; }

        public string? Pan { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? MaxEmployees { get; set; }

        public int ProductOwnerId { get; set; }
    }
}
