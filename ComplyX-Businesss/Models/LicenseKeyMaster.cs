using ComplyX_Businesss.Models;
using System.Text.Json.Serialization;

namespace ComplyX_Businesss.Models
{
    public class LicenseKeyMaster
    {
        public int LicenseId { get; set; }
        public int ProductOwnerId { get; set; }
        public string LicenseKey { get; set; }
        public string PlanType { get; set; }
        public int MaxActivations { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ProductOwners? ProductOwners { get; set; }
        [JsonIgnore]
        public virtual ICollection<LicenseActivation>? LicenseActivation { get; set; } = new List<LicenseActivation>();
        [JsonIgnore]
        public virtual ICollection<LicenseAuditLogs>? LicenseAuditLogs { get; set; } = new List<LicenseAuditLogs>();
    }
}
