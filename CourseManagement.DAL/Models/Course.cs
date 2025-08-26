using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Entitys
{
    public class Course
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id")]
        //[NoNumber]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public CourseCategory Category { get; set; }
        [Required]

        public DateTime StartDate { get; set; }
        [Required]

        public DateTime EndDate { get; set; }
        [Required]

        public bool IsActive { get; set; }
        [Required]

        public int InstructorId { get; internal set; }
        public Instructor? Instructor { get; set; }
        public class NoNumberAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null && value.ToString().Any(char.IsDigit))
                {
                    return new ValidationResult("Course name cannot contain numbers.");
                }
                return ValidationResult.Success;
            }
        }
    }
}
    

