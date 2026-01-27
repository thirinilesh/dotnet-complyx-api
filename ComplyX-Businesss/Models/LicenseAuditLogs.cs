using System.Text.Json.Serialization;

namespace ComplyX_Businesss.Models
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
        [JsonIgnore]
        public virtual LicenseKeyMaster? LicenseKeyMaster { get; set; }
        [JsonIgnore]
        public virtual LicenseActivation? LicenseActivation { get; set; }
    }
}
