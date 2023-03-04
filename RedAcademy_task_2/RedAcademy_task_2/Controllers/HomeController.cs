using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Dashboard_marketing()
        {
            return View();
        }

        public IActionResult Dashboard_finantial()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel(); if (id == 500)
            {
                modelError.Message = "An error has occurred! Please try again later or contact our support.";
                modelError.Title = "An error has occurred!";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "The page you are looking for does not exist!";
                modelError.Title = "Oops! Page not found!";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "You don't have permission to do this!";
                modelError.Title = "Access Denied!";
                modelError.ErrorCode = id;
            }
            else if (id == 401)
            {
                modelError.Message = "Ýou are not authorized to do this!";
                modelError.Title = "Access without authorization!";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(500);
            }
            return View("Error", modelError);
        }
    }
}