using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PlannerLib.DataBase;
using PlannerLib.Model;

namespace Planner.Controllers
{
    public class TaskController : Controller
    {

        MysqlController<Task> taskDAL = new MysqlController<Task>();

        [HttpGet]
        public IActionResult Main()
        {
            List<Task> tasks = taskDAL.Reed().ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Read()
        {
            List<Task> tasks = taskDAL.Reed().OrderBy(u => u.Priority).ToList();
            

            return View(tasks);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind] Task task)
        {

            if (ModelState.IsValid)
            {
                taskDAL.Create(task);
                return RedirectToAction("Main");
            }

            return View(task);
        }


        public IActionResult Edit(int id)
        {
            Task task = taskDAL.Reed(id);

            if (task == null) return NotFound();

            return View(task);
        }


        // кнопка delete should to be this

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Task task, int id = 0)
        {
            if (ModelState.IsValid)
            {
                taskDAL.Update(task);
                return RedirectToAction("Main");
            }

            return View(taskDAL);
        }


        public IActionResult Delete(int id = 0)
        {
            int b = taskDAL.Delete(id);
            return RedirectToAction("Main");

        }



    }
}
