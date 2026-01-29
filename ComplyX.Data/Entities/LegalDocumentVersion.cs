using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class LegalDocumentVersion
{
    public int VersionId { get; set; }

    public int DocumentId { get; set; }

    public string VersionNumber { get; set; } = null!;

    public string VersionType { get; set; } = null!;

    public string HtmlContent { get; set; } = null!;

    public string ContentHash { get; set; } = null!;

    public DateOnly EffectiveFromDate { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? ChangeSummary { get; set; }

    public string? LegalBasis { get; set; }

    public bool? IsPublished { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ApprovedAt { get; set; }

    public string? ApprovedBy { get; set; }

    public virtual LegalDocument Document { get; set; } = null!;

    public virtual ICollection<LegalDocumentAcceptance> LegalDocumentAcceptances { get; set; } = new List<LegalDocumentAcceptance>();
}
