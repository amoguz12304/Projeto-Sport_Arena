using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportArena.Models;

namespace SportArena.Controllers
{
    public class LojaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
