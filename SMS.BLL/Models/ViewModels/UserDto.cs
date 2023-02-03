using SMS.BLL.Empressions;
using System.ComponentModel.DataAnnotations;

namespace SMS.BLL.Models.ViewModels
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(30, ErrorMessage = "First name length must be 30 at most!")]
        [MinLength(3, ErrorMessage = "Last name length must be at least 3!")]
        [AlphabetOnly(ErrorMessage = "Please, enter only letters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(30, ErrorMessage = "Password length must be 30 at most!")]
        [MinLength(3, ErrorMessage = "Password length must be at least 3!")]
        [AlphabetOnly(ErrorMessage = "Please, enter only letters")]
        public string LastName { get; set;} = null!;

        [Required(ErrorMessage = "Email address is required")]
        [MaxLength(30, ErrorMessage = "Password length must be 30 at most!")]
        [MinLength(6, ErrorMessage = "Password length must be at least 6!")]
        [Email(ErrorMessage = "Please, enter valid email address!")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(30, ErrorMessage = "Password length must be 30 at most!")]
        [MinLength(6, ErrorMessage = "Password length must be at least 6!")]
        [Alphabet(ErrorMessage ="Password should contain at least 1 letter")]
        [Digit(ErrorMessage = "Password should contain at least 1 digit")]
        public string Password { get; set; } = null!;

        public string? Role { get; set; } = null!;
    }
}
