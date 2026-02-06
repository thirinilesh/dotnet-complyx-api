using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.LegalDocument
{
    public  class LegalDocumentResponseModel
    {
        public int DocumentId { get; set; }

        public string DocumentCode { get; set; } = null!;

        public string DocumentName { get; set; } = null!;

        public string? ApplicableRegion { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}
