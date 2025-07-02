using Microsoft.AspNetCore.Mvc;
using MVCTaskProject.Models;
using MVCTaskProject.Repository;

namespace MVCTaskProject.Controllers
{
    public class ProfileController : Controller
    { 
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var result = _profileRepository.GetUserProfile(userId.Value);
                if (result != null)
                {
                    return View(result); // Pass the DTO to the view
                }
                else
                {
                    Console.WriteLine($"Profile not found for UserId: {userId.Value}");
                }
            }
            else
            {
                Console.WriteLine("UserId not found in session.");
            }

            return RedirectToAction("Login", "Account"); // Redirect if user is not logged in or profile not found
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserProfileDto addProfile)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue && ModelState.IsValid)
            {
                var result = _profileRepository.AddUserProfile(addProfile, userId.Value);
                if (result)
                {
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(addProfile);
            }
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var result = _profileRepository.GetUserProfile(userId.Value);
                if (result != null)
                {
                    return View(result);
                }
            }

            return RedirectToAction("Login", "Account"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserProfileDto userProfileDto)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue && ModelState.IsValid)
            {
                var result = _profileRepository.EditUserProfile(userProfileDto, userId.Value);
                if (result)
                {
                    return RedirectToAction("Index"); 
                }
            }

            return View(userProfileDto); 
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profilePicture)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue && profilePicture != null && profilePicture.Length > 0)
            {
                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image");

                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }

              
                var filePath = Path.Combine(uploadsFolderPath, profilePicture.FileName);

               
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

               
                var userProfile = _profileRepository.GetUserProfile(userId.Value);
                if (userProfile != null)
                {
                    userProfile.ProfilePictureUrl = $"/image/{profilePicture.FileName}";
                    _profileRepository.EditUserProfile(userProfile, userId.Value);
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index"); 
        }

    }
}
