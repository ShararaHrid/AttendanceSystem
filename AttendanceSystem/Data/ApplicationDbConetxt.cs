using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models.Entities;

namespace AttendanceSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
