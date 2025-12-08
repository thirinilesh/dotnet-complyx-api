using System.ComponentModel.DataAnnotations;

namespace ComplyX.Models
{
    public class ResetPasswordRequestModel
    {
        public required string Username { get; init; }
        public string token { get; set; }


        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
