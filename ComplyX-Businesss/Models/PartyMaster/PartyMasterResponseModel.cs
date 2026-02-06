using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.PartyMaster
{
    public class PartyMasterResponseModel
    {
        public int PartyId { get; set; }

        public string PartyName { get; set; } = null!;

        public string? Pan { get; set; }

        public string? Gstin { get; set; }

        public string PartyType { get; set; } = null!;

        public string Address1 { get; set; } = null!;

        public string? Address2 { get; set; }

        public string City { get; set; } = null!;

        public string StateCode { get; set; } = null!;

        public string Pincode { get; set; } = null!;

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}
