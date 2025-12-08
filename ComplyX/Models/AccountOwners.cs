using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace ComplyX.Models
{
    public class ProductOwners
    {
        [Key]
        public int AccountOwnerId { get; set; }

        public string OwnerName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

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

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }

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
            RuleFor(x => x.AccountOwnerId);
           
        } 
        private bool BeValidEnum<TEnum>(string value) where TEnum : struct, Enum { return Enum.TryParse<TEnum>(value, true, out _); } }

}
