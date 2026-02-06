using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LegalDocumentAcceptance
{
    public class LegalDocumentAcceptanceResponseModel
    {
        public int AcceptanceId { get; set; }

        public string UserId { get; set; } = null!;

        public int DocumentId { get; set; }

        public int VersionId { get; set; }

        public DateTime AcceptedAt { get; set; }

        public string? AcceptedIp { get; set; }

        public string? AcceptedDevice { get; set; }

        public string? AcceptanceMethod { get; set; }

        public string ConsentProofHash { get; set; } = null!;
    }
}
