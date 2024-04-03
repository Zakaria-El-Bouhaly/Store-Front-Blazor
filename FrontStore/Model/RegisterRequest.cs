using System.ComponentModel.DataAnnotations;

namespace FrontStore.Model
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        [MinLength(3)]
        public string FullName { get; set; }
    }
}
