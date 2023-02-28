
using SMS.BLL.Empressions;
using System.ComponentModel.DataAnnotations;

namespace SMS.BLL.Models.AuthenticationModels
{
    public class SignInModel
    {
        [Required]
        [Email(ErrorMessage = "Please, valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(30, ErrorMessage = "Please, enter valid password")]
        [MinLength(6, ErrorMessage = "Please, enter valid password")]
        [Alphabet(ErrorMessage = "Please, enter valid password")]
        [Digit(ErrorMessage = "Please, enter valid password")]
        public string Password { get; set; } = null!;

    }
}
