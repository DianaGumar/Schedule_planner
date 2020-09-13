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
        MysqlController<Schedule> scheduleDAL = new MysqlController<Schedule>();

        [HttpGet]
        public IActionResult Main()
        {
            IList<Task> tasks = taskDAL.Reed().ToList();

            ViewData["TaskToday"] = GetTasksToday(tasks);
            ViewData["LimitForDay"] = 0;
            ViewData["Task"] = new Task();

            return View(tasks);
        }


        private IList<Task> GetTasksToday(IList<Task> tasks)
        {
            int[] tasksTodayId = scheduleDAL.Reed().Where(s =>
               s.Schedule_date.Date == DateTime.Now.Date).Select(i => i.Task_id).ToArray();

            IList<Task> tasksToday = new List<Task>();
            foreach (var item in tasksTodayId)
            {
                tasksToday.Add(tasks.First(t => t.Id == item));
            }

            return tasksToday;


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


        [HttpPost]
        public IActionResult CreateSmall([Bind] Task task)
        {
            if (ModelState.IsValid)
            {
                taskDAL.Create(task);
            }

            return RedirectToAction("Main");

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
            IEnumerable<Schedule> ss = scheduleDAL.Reed().Where(s => s.Task_id == id);
            foreach (var item in ss)
            {
                scheduleDAL.Delete(item.Id);
            }

            return RedirectToAction("Main");

        }


        public IActionResult AddToScheduler(int id = 0)
        {
            IList<Schedule> schedulespast =
                scheduleDAL.Reed().Where(s => s.Schedule_date.Date < DateTime.Now.Date).ToList();

            foreach (var item in schedulespast)
            {
                scheduleDAL.Delete(item.Id);
            }


            IList<Schedule> schedules = 
                scheduleDAL.Reed().Where(s => s.Schedule_date.Date == DateTime.Now.Date).ToList();



            if(schedules.Count < 3 && schedules.Where(s => s.Task_id == id).Count() == 0)
            {

                Schedule schedule = new Schedule();
                schedule.Task_id = id;
                schedule.Schedule_date = DateTime.Now;

                int b = scheduleDAL.Create(schedule);

                ViewData["LimitForDay"] = 0;
            }
            else
            {
                ViewData["LimitForDay"] = 1;
            }

            return RedirectToAction("Main");

        }

        public IActionResult DropFromScheduler(int id = 0)
        {
            IList<Schedule> schedules =
                scheduleDAL.Reed().Where(s => s.Task_id == id).ToList();

            foreach (var item in schedules)
            {
                scheduleDAL.Delete(item.Id);
            }
            
            return RedirectToAction("Main");
        }



    }
}
