using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCTaskProject.Models;
using MVCTaskProject.Repository;
using System.Diagnostics.Eventing.Reader;

namespace MVCTaskProject.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if(userId.HasValue)
            {
                var result = _homeRepository.GetUserTasks(userId.Value); 
                //var result = _homeRepository.GetUserTask();
                return View(result);
            }
            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        public IActionResult Add()
        {
           
            return View();
        }

        [HttpPost("Add")]
        public IActionResult Add(AddUsersTasks addTask)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue && ModelState.IsValid)
            {
                var result = _homeRepository.AddUserTasks(addTask, userId.Value);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(addTask);
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var result = _homeRepository.Update(id);
            return View(result);
        }
        [HttpPost]
        public IActionResult Update(UserTasksDto userTasksDto)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if ( userId.HasValue && ModelState.IsValid)
            {
                var result = _homeRepository.UpdateUserTask(userTasksDto, userId.Value);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(userTasksDto);
                }
            }
            else
            {
                return View(userTasksDto);
            }
        }

        public IActionResult Delete(int id)
        {
            bool record = _homeRepository.DeleteUserTask(id);
            return RedirectToAction("Index", "Home");
        }

     
    }
}
