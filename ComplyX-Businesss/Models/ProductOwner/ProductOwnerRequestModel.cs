using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models.ProductOwner
{
    public class ProductOwnerRequestModel
    {
        [Key]
        public int ProductOwnerId { get; set; }
        public string? OwnerName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string LegalName { get; set; } = string.Empty;
        public string RegistrationId { get; set; } = string.Empty;
        public string OrganizationType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string? SubscriptionPlan { get; set; } = string.Empty;
        public DateTime? SubscriptionStart { get; set; } = DateTime.UtcNow;
        public DateTime? SubscriptionExpiry { get; set; } = DateTime.UtcNow;
        public int? MaxCompanies { get; set; }
        public int? MaxUsers { get; set; }
        public int? MaxStorageMB { get; set; }
        public bool? AllowCloudBackup { get; set; }
        public bool? AllowGSTModule { get; set; }
        public bool? AllowTDSModule { get; set; }
        public bool? AllowCLRAModule { get; set; }
        public bool? AllowPayrollModule { get; set; }
        public bool? AllowDSCSigning { get; set; }
    }
}
