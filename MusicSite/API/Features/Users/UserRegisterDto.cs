using System.ComponentModel.DataAnnotations;

namespace MusicSite.API.Features
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Password Confirmation")]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; } = string.Empty;
    }
}
