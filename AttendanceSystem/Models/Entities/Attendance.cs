using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceSystem.Models.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public Student Student { get; set; }  // navigation property
    }
}
