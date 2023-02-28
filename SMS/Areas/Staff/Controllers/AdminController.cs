using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMS.BLL.Models.ViewModels;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.Extensions;
using SMSCore.Models.Entities;
using StudentManagementSystem.Controllers;

namespace SMS.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class AdminController : CustomControllerBase
    {
        private readonly IStudentService _studentService;
        public AdminController
        (
            IWebHostEnvironment hostEnvironment,
            ILogger<AdminController> logger,
            IMapper mapper,
            IStudentService studentService

        ) : base(hostEnvironment, logger, mapper)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Students")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAll(User.GetUserId());

            return View(students);
        }

        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(StudentDto model) 
        {
            if(ModelState.IsValid)
            {
                var student = await _studentService.CreateAsync(User.GetUserId(), Mapper.Map<Student>(model));

                if(student == null) { return View(student); }

                return RedirectToAction("Students");
            }

            return View(model);
        }
    }
}
