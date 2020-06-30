using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Planner.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Reg(int age)
        {

            ViewData["Age"] = age;

            return View();

        }

        public IActionResult Error()
        {
            return NotFound();
            //return View();
        }
    }
}