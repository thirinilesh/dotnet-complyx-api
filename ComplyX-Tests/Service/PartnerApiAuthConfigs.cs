
public partial class PartnerApiAuthConfig
{
    public int Id { get; set; }

    public int PartnerId { get; set; }

    public string ApiUserId { get; set; } = null!;

    public string PublicAccessKey { get; set; } = null!;

    public bool Active { get; set; }

    public string CreatedByUserId { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedByUserId { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

  
}
