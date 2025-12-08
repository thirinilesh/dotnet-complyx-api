using FluentValidation.Results;
using ComplyX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
namespace ComplyX.Models
{
    public class AuthRequestModel
    {
        public string Token { get; set; }
        //public string RefreshToken { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string  Message { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool HasSensitiveDataAccess { get; set; }
        //public UserDefaultSettingResponseModel UserDefaultSetting { get; set; }
        //public CustomerResponseModel Customer { get; set; }
        public List<string> Roles { get; set; }
        public bool IsLoggedInWithSso { get; set; } = false;
        public int SsoUserTaxYearClaim { get; set; } = 0;
        public string GrantType { get; set; }
        public string Assertion { get; set; }
    }
    public class ErrorResponse
    {
        public string Error { get; set; }
        public int Code { get; set; }
    }

    public class AuthRequestModelValidator : AbstractValidator<AuthRequestModel>
    {
        public AuthRequestModelValidator()
        {
            RuleFor(x => x.GrantType)
                .NotEmpty().WithMessage("GrantType is required.")
                .Equal("urn:ietf:params:oauth:grant-type:jwt-bearer").WithMessage("Invalid GrantType.");

            RuleFor(x => x.Assertion)
                .NotEmpty().WithMessage("Assertion is required.");
        }
    }

}
