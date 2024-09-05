using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.web.Data;
using StudentManagement.web.Models;
using StudentManagement.web.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
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
        public async Task<IActionResult> Edit(int Id)
        {
            var students = await dbContext.Students.FindAsync(Id);
            return View(students);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Students.FindAsync(viewModel.Id);
            
            if(student != null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");

        }
        [HttpPost]
        public async Task<IActionResult>Delete(Student viewModel)
        {
            var student = await dbContext.Students.
                AsNoTracking()
                .FirstOrDefaultAsync(x =>x.Id == viewModel.Id);

            if(student != null)
            {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");

        }

    }
}
