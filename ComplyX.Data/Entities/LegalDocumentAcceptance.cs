using System;
using System.Collections.Generic;

namespace ComplyX.Data.Entities;

public partial class LegalDocumentAcceptance
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

    public virtual LegalDocument Document { get; set; } = null!;

    public virtual LegalDocumentVersion Version { get; set; } = null!;
}
