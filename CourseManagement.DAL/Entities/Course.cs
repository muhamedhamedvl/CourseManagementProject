using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty; // unique + custom nonumber () ً لاحقا
        [Required]
        public string Category { get; set; } = string.Empty;
        public int? InstructorId { get; set; }
        public AppUser? Instructor { get; set; }
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
