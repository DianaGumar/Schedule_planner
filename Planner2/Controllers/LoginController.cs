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
        public IActionResult Reg()
        {
            return View();
        }

        //("name", "password", "email", "phone")
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reg([Bind] User user)
        {

            //if (user.Name.Length > 5)
            //{
            //    ModelState.AddModelError("Name", "to long");
            //}
            if (ModelState.IsValid)
            {
                userDAL.Create(user);
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Some wrong " + user.ToString();
            }

            return View(user);
        }

        
        public IActionResult Edit(int id)
        {
            User user = userDAL.Reed(id);

            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] User user, int id = 0)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "Valid";
                userDAL.Update(user);
                return RedirectToAction("Login");
            }
            ViewBag.Message = "No valid";
            return View(userDAL);
        }


        public IActionResult Delete(int id = 0)
        {
            int b = userDAL.Delete(id);
            return RedirectToAction("Login");

        }



        public IActionResult Error()
        {
            return NotFound();
            //return View();
        }
    }
}