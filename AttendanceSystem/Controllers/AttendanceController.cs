using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public AttendanceController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddAttendanceViewModel addAttendanceViewModel)
        {
            try
            {
                var attendance = new Attendance
                {
                    Id = addAttendanceViewModel.Id,
                    StudentId = addAttendanceViewModel.StudentId
                };
                await dbContext.Attendance.AddAsync(attendance);
                await dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var attendance = await dbContext.Attendance.ToListAsync();
            return View(attendance);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attendance = await dbContext.Attendance.FindAsync(id);
            return View(attendance);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Attendance edit)
        {
            var attendance = await dbContext.Attendance.FindAsync(edit.Id);
            if (attendance != null)
            {
                attendance.Id = edit.Id;
                attendance.StudentId = edit.StudentId;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Attendance"); //action method=list, controller=attendance
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Attendance delete)
        {
            var attendance = await dbContext.Attendance.AsNoTracking().FirstOrDefaultAsync(x => x.Id == delete.Id);
            if (attendance != null)
            {
                dbContext.Attendance.Remove(delete);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Attendance"); //action method=list, controller=attendance
        }
    }
}
