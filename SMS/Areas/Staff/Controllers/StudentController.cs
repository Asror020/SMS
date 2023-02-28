using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMS.BLL.Models.ViewModels;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.Extensions;
using SMSCore.Enums;
using SMSCore.Models.Entities;
using StudentManagementSystem.Controllers;

namespace SMS.Areas.Staff.Controllers
{
    [Area("Staff")]
    public class StudentController : CustomControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly ISelectionItemService _selectionItemService;

        public StudentController(
            IWebHostEnvironment hostEnvironment,
            ILogger<StudentController> logger,
            IMapper mapper,
            IStudentService studentService,
            IGroupService groupService
,
            ISelectionItemService selectionItemService) : base(hostEnvironment, logger, mapper)
        {
            _studentService = studentService;
            _groupService = groupService;
            _selectionItemService = selectionItemService;
        }

        public async Task<IActionResult> Add()
        {
            var model = new StudentDto();

            model.CourseLevelsList = _selectionItemService.GetByType(SelectionItemTypes.CourseLevel.ToString());

            model.DegreesList = _selectionItemService.GetByType(SelectionItemTypes.Degree.ToString());

            model.GroupsList = (await _groupService.GetGroupNames(User.GetUserId())).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentDto model)
        {
            model.CourseLevelsList = _selectionItemService.GetByType(SelectionItemTypes.CourseLevel.ToString());

            model.DegreesList = _selectionItemService.GetByType(SelectionItemTypes.Degree.ToString());

            model.GroupsList = (await _groupService.GetGroupNames(User.GetUserId())).ToList();

            if (ModelState.IsValid)
            {
                var student = await _studentService.CreateAsync(User.GetUserId(), Mapper.Map<Student>(model));

                if (student == null) { return View(student); }

                if (student.User.EmailAddress == null)
                {
                    TempData["Email"] = "This email address is in use!";

                    return View(model);
                }

                return RedirectToAction("Students", "Admin", new { area = "Staff" });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(long id)
        {
            var model = Mapper.Map<StudentDto>(await _studentService.GetByIdAsync(id));

            return View(model);
        }

        public async Task<IActionResult> Update(long id)
        {
            var model = Mapper.Map<StudentDto>(await _studentService.GetByIdAsync(id));

            model.CourseLevelsList = _selectionItemService.GetByType(SelectionItemTypes.CourseLevel.ToString());

            model.DegreesList = _selectionItemService.GetByType(SelectionItemTypes.Degree.ToString());

            model.GroupsList = (await _groupService.GetGroupNames(User.GetUserId())).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, StudentDto model)
        {
            model.CourseLevelsList = _selectionItemService.GetByType(SelectionItemTypes.CourseLevel.ToString());

            model.DegreesList = _selectionItemService.GetByType(SelectionItemTypes.Degree.ToString());

            model.GroupsList = (await _groupService.GetGroupNames(User.GetUserId())).ToList();

            if (ModelState.IsValid)
            {
                await _studentService.UpdateAsync(id, Mapper.Map<Student>(model));

                return RedirectToAction(nameof(Details), new {id = id});
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _studentService.DeleteAsync(id);

            return RedirectToAction("Students", "Admin", new { area = "Staff" });
        }
    }
}
