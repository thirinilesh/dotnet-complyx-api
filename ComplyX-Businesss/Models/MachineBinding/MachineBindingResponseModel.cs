using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.MachineBinding
{
    public class MachineBindingResponseModel
    {
        public int MachineBindingId { get; set; }

        public string MachineHash { get; set; } = null!;

        public string? Cpuid { get; set; }

        public string? MotherboardSerial { get; set; }

        public string? Macaddresses { get; set; }

        public string? WindowsSid { get; set; }

        public DateTime FirstSeenAt { get; set; }

        public DateTime? LastSeenAt { get; set; }

        public int LicenseActivationId { get; set; }
    }
}
