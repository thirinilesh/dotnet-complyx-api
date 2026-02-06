using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LicenseActivation
{
    public class LicenseActivationRequestModel
    {
        public int ActivationId { get; set; }

        public int LicenseId { get; set; }

        public string MachineHash { get; set; } = null!;

        public string? MachineName { get; set; }

        public string? OsUser { get; set; }

        public DateTime ActivatedAt { get; set; }

        public DateTime? LastVerifiedAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? GraceExpiryAt { get; set; }

        public string? AppVersion { get; set; }
    }
}
