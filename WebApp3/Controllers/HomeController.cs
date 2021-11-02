using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp3.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        [Route("home/index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("home/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
