namespace ComplyX.Models
{
    public class LicenseActivation
    {
        public int ActivationId {  get; set; }
        public int LicenseId { get; set; }
        public string MachineHash { get; set; }
        public string? MachineName { get; set; }
        public string? OsUser { get; set; }
        public DateTime ActivatedAt { get; set; }
        public DateTime? LastVerifiedAt { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? GraceExpiryAt { get; set; }
        public string? AppVersion { get; set; }

        public virtual LicenseKeyMaster? LicenseKeyMaster { get; set; }
        public virtual ICollection<LicenseAuditLogs>? LicenseAuditLogs { get; set; } = new List<LicenseAuditLogs>();
    }
}
