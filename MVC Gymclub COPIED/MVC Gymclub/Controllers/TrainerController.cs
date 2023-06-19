using Microsoft.AspNetCore.Mvc;
using MVC_Gymclub.ViewModels;
using System.Diagnostics;

namespace MVC_Gymclub.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ILogger<TrainerController> _logger;

        public TrainerController(ILogger<TrainerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}