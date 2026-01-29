using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class LegalDocument
{
    public int DocumentId { get; set; }

    public string DocumentCode { get; set; } = null!;

    public string DocumentName { get; set; } = null!;

    public string? ApplicableRegion { get; set; }

    public string? Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual ICollection<LegalDocumentAcceptance> LegalDocumentAcceptances { get; set; } = new List<LegalDocumentAcceptance>();

    public virtual ICollection<LegalDocumentVersion> LegalDocumentVersions { get; set; } = new List<LegalDocumentVersion>();
}
