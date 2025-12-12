using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace ComplyX.Models
{
    public class ProductOwners
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

        public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
        public virtual ICollection<ProductOwnerSubscriptions> ProductOwnerSubscriptionss { get; set; } = new List<ProductOwnerSubscriptions>();
        internal FluentValidation.Results.ValidationResult Validate(ProductOwners productOwners)
        {
            throw new NotImplementedException();
        }
    }
    public class ProductOwnersValidater : AbstractValidator<ProductOwners>
    { 
        public ProductOwnersValidater() 
        {
            RuleFor(x => x.City);
            RuleFor(x => x.Country);
            RuleFor(x => x.Address);       
            RuleFor(x => x.Email);
            RuleFor(x => x.ProductOwnerId);
           
        } 
        private bool BeValidEnum<TEnum>(string value) where TEnum : struct, Enum { return Enum.TryParse<TEnum>(value, true, out _); } }

}
