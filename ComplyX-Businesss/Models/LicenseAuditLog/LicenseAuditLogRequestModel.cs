using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LicenseAuditLog
{
    public class LicenseAuditLogRequestModel
    {
        public int AuditId { get; set; }

        public int LicenseId { get; set; }

        public int? ActivationId { get; set; }

        public string? EventType { get; set; }

        public string? EventMessage { get; set; }

        public DateTime LoggedAt { get; set; }

        public string? MachineHash { get; set; }

        public string? Ipaddress { get; set; }
    }
}
