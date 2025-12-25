using System.ComponentModel.DataAnnotations;

namespace ComplyX_Businesss.Models
{
    public class RegisterUser
    {
        [Key]
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string GSTIN { get; set; } = string.Empty;
        public string PAN { get; set; } = string.Empty;
    }
}
