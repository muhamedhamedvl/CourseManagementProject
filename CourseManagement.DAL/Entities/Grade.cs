using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DAL.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        [Range(0, 100)]
        public int Value { get; set; }
        [Required]
        public int SessionId { get; set; }
        public Session? Session { get; set; }

        [Required]
        public int TraineeId { get; set; }
        public AppUser? Trainee { get; set; }
    }
}
