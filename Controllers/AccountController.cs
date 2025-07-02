using Microsoft.AspNetCore.Mvc;
using MVCTaskProject.Models;
using MVCTaskProject.Repository;
using MVCTaskProject.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Tls;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace MVCTaskProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ILogger<AccountController> _logger;
        private readonly IMailService _mailService;

        public AccountController(ILoginRepository loginRepository, ILogger<AccountController> logger, IMailService mailService)
        {
            _loginRepository = loginRepository;
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterDto registerDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _loginRepository.RegisterUser(registerDto);
                    if (result)
                    {
                        return RedirectToAction("Login","Account");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "User already exists.";
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(registerDto);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var result = _loginRepository.LoginUser(userDto);
                if (result)
                {
                    var user = _loginRepository.GetUserByEmail(userDto.Email);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid email or password. Please try again.";
                }
            }
            return View(userDto);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]

        public IActionResult SendOtp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendOtp(ForgotPassword forgotPassword)
        {
            if (ModelState.IsValid)
            {
                var user = _loginRepository.GetUserByEmail(forgotPassword.Email);
                if (user != null)
                {
                    var otp = _loginRepository.GenerateOtp(forgotPassword.Email);
                    bool isEmailSent = _mailService.SendEmail(new SendEmail
                    {
                        Email = forgotPassword.Email,
                        Subject = "Your OTP Code",
                        Body = $"Your OTP code is: {otp}"
                    });

                    if (isEmailSent)
                    {
                        HttpContext.Session.SetString("Otp", otp);
                        HttpContext.Session.SetString("UserEmail", forgotPassword.Email);
                        return RedirectToAction("VerifyOtp");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Failed to send OTP. Please try again.";
                    }
                }
                else
                {
                    ViewData["ErrorMessage"] = "Email not found.";
                }
            }
            return View(forgotPassword);
        }

        [HttpGet]
        public IActionResult VerifyOtp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyOtp(string otpInput)
        {
            var storedOtp = HttpContext.Session.GetString("Otp");
            var email = HttpContext.Session.GetString("UserEmail");

            if (otpInput == storedOtp)
            {
                HttpContext.Session.Remove("Otp");
                HttpContext.Session.Remove("UserEmail");
                return RedirectToAction("ChangePassword");
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid OTP.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.Session.GetString("UserEmail");
                bool result = _loginRepository.ChangePassword(changePasswordDto);

                if (result)
                {
                    TempData["SuccessMessage"] = "Your password has been changed successfully.";
                    return RedirectToAction("Login"); 
                }
                else
                {
                    ViewData["ErrorMessage"] = "Current password is incorrect."; 
                }
            }

            return View(changePasswordDto);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}