using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestSharp;
using SMS.BLL.Models.AuthenticationModels;
using SMS.BLL.Models.ViewModels;
using SMS.BLL.Services.AuthenticationServices.Interfaces;
using SMS.BLL.Services.EntityServices.Interfaces;
using SMS.Extensions;
using SMSCore.Enums;
using SMSCore.Models.Entities;
using StudentManagementSystem.Controllers;
using System.Security.Claims;

namespace SMS.Controllers
{
    [Area("Authentication")]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthenticaitonService _authService;
        private readonly IUserService _userService;
        private readonly IUniversityService _universityService;
        private readonly IVerificationService _verificationService;
        public AuthController
        (
            IWebHostEnvironment hostEnvironment,
            ILogger<AuthController> logger,
            IAuthenticaitonService authService,
            IUserService userService,
            IUniversityService universityService,
            IMapper mapper
,
            IVerificationService verificationService) : base(hostEnvironment, logger, mapper)
        {
            _authService = authService;
            _userService = userService;
            _universityService = universityService;
            _verificationService = verificationService;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _authService.SignIn(model);

                if (data.Principal == null) { TempData["Error"] = "Invalid email or password"; return View(); }

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, data.Principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(24)
                });

                if (data.Role == UserRoles.Admin.ToString()) return Redirect("~/administration/admin/index");

                else if (data.Role == UserRoles.Staff.ToString() ||
                    data.Role == UserRoles.Owner.ToString()) return Redirect("~/staff/admin/index");

                else return Redirect("~/students/student/index");
            }

            else return View();
        }

        [ActionName("SignOut")]
        [AllowAnonymous]
        public async Task<IActionResult> SignOutAsync()
        {
            var user = User.Identity;

            await HttpContext.SignOutAsync("JwtAuth");

            return Redirect("~/Home/Index");
        }

        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(UserDto model)
        {
            if (ModelState.IsValid)
            {
                var data = await _userService.CreateAsync(Mapper.Map<User>(model));

                if(data.EmailAddress == null) 
                { 
                    TempData["Error"] = "This email address is not available!";
                    return View(); 
                }

                Response.Cookies.Append("userId", data.Id.ToString());

                return RedirectToAction("RegisterUniversity");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult RegisterUniversity()
        {

            var userId = Request.Cookies.FirstOrDefault(x => x.Key == "userId").Value;

            if (string.IsNullOrEmpty(userId) || userId == null) return RedirectToAction(nameof(RegisterUser));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUniversity(UniversityDto model)
        {
            if (ModelState.IsValid)
            {
                var userId = Request.Cookies.FirstOrDefault(x => x.Key == "userId").Value;

                if (string.IsNullOrEmpty(userId) || userId == null) return RedirectToAction(nameof(RegisterUser));

                Response.Cookies.Delete("userId");

                model.UniversityAdminUserId = Convert.ToInt64(userId);
                var data = await _universityService.CreateAsync(Mapper.Map<University>(model));

                if(data.Name == null)
                {
                    TempData["Error"] = "This university already exists!";
                    return View();
                }

                return Redirect($"SendVerification/{data.UniversityAdminUserId}");
            }
            else return View();
        }

        [AllowAnonymous]
        [Route("Authentication/auth/SendVerification/{userId}")]
        public async Task<IActionResult> SendVerification([FromRoute]long userId)
        {
            await _verificationService.SendEmailAsync(userId);

            return View();
        }


        [AllowAnonymous]
        [Route("auth/verify/{pinCode}")]
        public async Task<IActionResult> Verify([FromRoute]long pinCode)
        {
            await _verificationService.VerifyAsync(pinCode);

            return View();
        }
    }
}
