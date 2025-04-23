using AttendanceSystem.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models
{
    public class AddAttendanceViewModel
    {
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
