using SMSCore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SMS.BLL.Services.EntityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SMS.BLL.Models.ViewModels;
using AutoMapper;

namespace StudentManagementSystem.Controllers
{
    public class UsersController : CustomControllerBase
    {
        private readonly IUserService _userService;
        public UsersController
        (
            IWebHostEnvironment hostEnvironment,
            ILogger<User> logger,
            IUserService userService, 
            IMapper mapper
        ) : base(hostEnvironment, logger, mapper)
        {
            _userService = userService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto user)
        {
            if (ModelState.IsValid)
            {
                var data = await _userService.CreateAsync(Mapper.Map<User>(user));

                return data != null ? Ok(data) : RedirectToAction(nameof(Create));
            }
            else return View();
        }

        public async Task<IActionResult> Update(long id)
        {
            var data = await _userService.GetByIdAsync(id);

            return data != null ? View(data) : RedirectToAction(nameof(Create));
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, User user)
        {

            if (ModelState.IsValid)
            {
                var data = await _userService.UpdateAsync(id, user);

                return data ? RedirectToAction(nameof(Create)) : RedirectToAction(nameof(Update));
            }
            else return View();
        }
        public async Task<IActionResult> Delete(long id)
        {
            var data = await _userService.GetByIdAsync(id);

            return data != null ? View(data) : RedirectToAction(nameof(Create));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteById(long id)
        {
            var data = await _userService.DeleteAsync(id);

            return data ? RedirectToAction("Create") : RedirectToAction(nameof(Delete));
        }
    }
}
