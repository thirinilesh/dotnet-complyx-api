using System.ComponentModel.DataAnnotations;

namespace ComplyX_Businesss.Models
{
    public class ForgotPasswordVerifyModel
    {
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; } = null!;
    }
}
