namespace ComplyX.Models
{
    public class LicenseAuditLogs
    {
        public int AuditId { get; set; }
        public int LicenseId {  get; set; }
        public int? ActivationId { get; set; }
        public string? EventType { get; set; }
        public string? EventMessage { get; set; }
        public DateTime LoggedAt { get; set; }
        public string? MachineHash { get; set; }
        public string? IPAddress { get; set; }

        public virtual LicenseKeyMaster? LicenseKeyMaster { get; set; }
        public virtual LicenseActivation? LicenseActivation { get; set; }
    }
}
