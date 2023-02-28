using SMS.BLL.Empressions;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMSCore.Enums;
using SMSCore.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Models.ViewModels
{
    public class StudentDto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Student Id is required!")]
        [AlphabetAndDigitOnly(ErrorMessage = "Enter digit or letter!")]
        [Display(Name = "Student Id")]
        public string StudentId { get; set; } = null!;

        [Required]
        [Display(Name = "Course Level")]
        public string CourseLevel { get; set; } = null!;

        [Required(ErrorMessage = "Degree is required!")]
        public string Degree { get; set; } = null!;

        [Required(ErrorMessage = "The field is required!")]
        [Display(Name = "Date of birth")]
        public DateOnly DateOfBirth { get; set; }

        [Display(Name = "Group name")]
        public string? GroupName { get; set; }

        public UserDto User { get; set; } = null!;

        public IList<string>? GroupsList { get; set; }

        public IList<SelectionItem>? CourseLevelsList { get; set; }

        public IList<SelectionItem>? DegreesList { get; set; }
    }
}
