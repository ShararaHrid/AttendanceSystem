using AttendanceSystem.Data;
using AttendanceSystem.Models;
using AttendanceSystem.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext= dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel addStudentViewModel )
        {
            var student = new Student
            {
                Id = addStudentViewModel.Id,
                Name = addStudentViewModel.Name,
                Roll = addStudentViewModel.Roll
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync(); 
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student=await dbContext.Students.FindAsync(id);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student edit)
        {
            var student = await dbContext.Students.FindAsync(edit.Id);
            if (student != null) { 
                student.Id = edit.Id;
                student.Name = edit.Name;
                student.Roll = edit.Roll;
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List","Student"); //action method=list, controller=student
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student delete)
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id==delete.Id);
            if (student != null) {
                dbContext.Students.Remove(delete);
                await dbContext.SaveChangesAsync(); 
            }
            return RedirectToAction("List","Student"); //action method=list, controller=student
        }
    }
}
