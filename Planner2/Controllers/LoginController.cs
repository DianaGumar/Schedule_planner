using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlannerLib.DataBase;
using PlannerLib.Model;

namespace Planner.Controllers
{
    public class LoginController : Controller
    {

        MysqlController<User> userDAL = new MysqlController<User>();

        public IActionResult Login()
        {
            List<User> users = userDAL.Reed().ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult Reg(int age)
        {

            ViewData["Age"] = age;

            return View();

        }

        [HttpPost]
        public IActionResult Reg([Bind] User user)
        {

            if (ModelState.IsValid)
            {
                userDAL.Create(user);
                return RedirectToAction("Login");
            }

            return View(user);
        }

        
        public IActionResult Error()
        {
            return NotFound();
            //return View();
        }
    }
}