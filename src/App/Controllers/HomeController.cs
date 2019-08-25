using App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 403)
            {
                modelError.Message = "You are not allowed.";
                modelError.Title = "Access denied.";
                modelError.ErrorCode = id.ToString();
            }
            else if (id == 404)
            {
                modelError.Message = "This page doesn't exists.";
                modelError.Title = "Page not  found.";
                modelError.ErrorCode = id.ToString();
            }
            else if (id == 500)
            {
                modelError.Message = "Something goes wrong. ";
                modelError.Title = "An error occour.";
                modelError.ErrorCode = id.ToString();
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelError);
        }
    }
}
