using FluentValidation;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ComplyX_Businesss.Models
{
    public class UpdateCompanyRequest
    {
        public string SourceId { get; set; }
        public string SourceCompanyId { get; set; }
        public string UserName { get; set; } = "";
        public string LegalName { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Ein { get; set; } = "";
        public string Address { get; set; } = "";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Contact { get; set; } = "";
        public string Email { get; set; } = "";
        public bool School { get; set; }
        public string WageType { get; set; }
        public string MeasurementType { get; set; }
        public string GroupType { get; set; }
        public bool GroupParent { get; set; } = false;
        public int? ParentCompanyId { get; set; }
        public bool Active { get; set; }
    }

    public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
    {
        public UpdateCompanyRequestValidator()
        {
            RuleFor(x => x.WageType).Must(BeValidEnum<AceType>).WithMessage("Invalid Wage Type")
                .When(x => !string.IsNullOrEmpty(x.WageType));
            RuleFor(x => x.MeasurementType).NotEmpty().WithMessage("MeasurementType is required");
            _ = RuleFor(x => x.MeasurementType).Must(BeValidEnum<MeasurementType>).WithMessage("Invalid Measurement Type")
                .When(x => !string.IsNullOrEmpty(x.MeasurementType));
            RuleFor(x => x.GroupType).NotEmpty().WithMessage("GroupType is required");
            RuleFor(x => x.GroupType).Must(BeValidEnum<GroupType>).WithMessage("Invalid Group Type")
                .When(x => !string.IsNullOrEmpty(x.GroupType));
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid Email format").When(x => !string.IsNullOrWhiteSpace(x.Email));
            RuleFor(x => x.School).NotNull().WithMessage("School is required");
            RuleFor(x => x.SourceCompanyId).NotEmpty().WithMessage("SourceCompanyId is required when adding a SourceId")
                .When(x => !string.IsNullOrEmpty(x.SourceId));
            RuleFor(x => x.SourceId).NotEmpty().WithMessage("SourceId is required when adding a SourceCompanyId")
                .When(x => !string.IsNullOrEmpty(x.SourceCompanyId));
        }
        public static bool BeValidEnum<TEnum>(string value) where TEnum : struct, Enum
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Enum.TryParse<TEnum>(value, out _);
        }
        public  enum MeasurementType
        {
            LookBack,
            Monthly
        }
    }
}
