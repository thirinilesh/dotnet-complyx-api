using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.Tdsdeductor
{
    public class TdsdeductorResponseModel
    {
        public int DeductorId { get; set; }

        public int CompanyId { get; set; }

        public string DeductorName { get; set; } = null!;

        public string? Tan { get; set; }

        public string? Pan { get; set; }

        public string DeductorCategory { get; set; } = null!;

        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Pincode { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Aocode { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
