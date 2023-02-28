using SMS.BLL.Empressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Models.ViewModels
{
    public class UniversityDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(64, ErrorMessage = "Length should be smaller than 65")]
        [MinLength(3, ErrorMessage = "Name is required")]
        [AlphabetOnly(ErrorMessage = "Please, enter only letters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(100, 100000)]
        [Display(Name = "Number of students")]
        public int NumberOfStudents { get; set; }

        public int NumberOfStaff { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [AlphabetOnly(ErrorMessage = "Please, enter only letters")]
        public string? Location { get; set; }

        public long UniversityAdminUserId { get; set; }
    }
}
