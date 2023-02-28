using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.Extensions;
using SMSCore.Models.Entities;
using StudentManagementSystem.Controllers;

namespace SMS.Areas.Admin.Controllers
{
    [Area("Administration")]
    public class AdminController : CustomControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUniversityService _universityService;
        public AdminController(
            IWebHostEnvironment hostEnvironment, 
            ILogger<AdminController> logger,
            IMapper mapper,
            IUserService userService,
            IUniversityService universityService) 
            : base(hostEnvironment, logger, mapper)
        {
            _userService = userService;
            _universityService = universityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("new-users")]
        public async Task<IActionResult> GetNewUsers()
        {

            var users = await _userService.GetNewUsersAsync();

            return View(users);
        }

        [ActionName("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync(User.GetUserId());

            return View(users);
        }

        public async Task<IActionResult> Approve(long id)
        {
            await _userService.ApproveAsync(id);

            return RedirectToAction("new-users");
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _userService.DeleteAsync(id);
            await _universityService.DeleteByOwnerId(id);

            return RedirectToAction("new-users");
        }
    }
}
