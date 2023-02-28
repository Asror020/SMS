using SMS.BLL.Empressions;
using System.ComponentModel.DataAnnotations;

namespace SMS.BLL.Models.ViewModels
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [MaxLength(65, ErrorMessage = "Length must be 65 at most!")]
        [MinLength(3, ErrorMessage = "First name is required!")]
        [AlphabetOnly(ErrorMessage = "Please, enter only letters!")]
        [Display(Name = "First name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required!")]
        [MaxLength(65, ErrorMessage = "Length must be 65 at most!")]
        [MinLength(3, ErrorMessage = "Last name is required!")]
        [AlphabetOnly(ErrorMessage = "Please, enter only letters!")]
        [Display(Name = "Last name")]
        public string LastName { get; set;} = null!;

        [Required(ErrorMessage = "Email address is required!")]
        [MaxLength(30, ErrorMessage = "Length must be 30 at most!")]
        [MinLength(6, ErrorMessage = "Email address is required!")]
        [Email(ErrorMessage = "Please, enter valid email address!")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Password is required!")]
        [MaxLength(30, ErrorMessage = "Length must be 30 at most!")]
        [MinLength(6, ErrorMessage = "Length must be at least 6!")]
        [Alphabet(ErrorMessage ="Must contain at least 1 letter!")]
        [Digit(ErrorMessage = "Must contain at least 1 digit!")]
        public string Password { get; set; } = null!;

        public string? Role { get; set; } = null!;
    }
}
