using System.ComponentModel.DataAnnotations;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty; // unique
        [Required]
        public string Role { get; set; }
        public ICollection<Course> CoursesTaught { get; set; } = new
        List<Course>();  
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
