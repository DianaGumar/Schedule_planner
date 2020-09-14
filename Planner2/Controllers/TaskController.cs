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

        public TaskController()
        {
            taskDAL = new MysqlController<Task>();
            scheduleDAL = new MysqlController<Schedule>();

            ViewData["LimitForDay"] = 0;
            @ViewData["Sync"] = new Planner.Models.CalendarId();



            //перенести в событие

            IList<Schedule> schedulespast =
               scheduleDAL.Reed().Where(s => s.Schedule_date.Date < DateTime.Now.Date).ToList();

            foreach (var item in schedulespast)
            {
                scheduleDAL.Delete(item.Id);

                if (item.Make == 1)
                {
                    taskDAL.Delete(item.Task_id);
                }
            }


        }

        MysqlController<Task> taskDAL;
        MysqlController<Schedule> scheduleDAL;

        [HttpGet]
        public IActionResult Main()
        {
            IList<Task> tasks = taskDAL.Reed().ToList();

            ViewData["TaskToday"] = GetTasksToday(tasks);

            
            //ViewData["Task"] = new Task();

            return View(tasks);
        }



        private void Maked(IEnumerable<Schedule> s)
        {

            Dictionary<int, int> make = new Dictionary<int, int>();
            foreach (var item in s)
            {
                make.Add(item.Task_id, item.Make);
            }

            ViewData["Maked"] = make;

        }


        private IList<Task> GetTasksToday(IList<Task> tasks)
        {
            IEnumerable<Schedule> s = scheduleDAL.Reed();
            Maked(s);

            int[] tasksTodayId = s.Select(i => i.Task_id).ToArray();

            IList<Task> tasksToday = new List<Task>();

            foreach (var item in tasksTodayId)
            {
                tasksToday.Add(tasks.First(t => t.Id == item));
            }
           
            if (tasksTodayId.Count() == 3)
            {
                ViewData["LimitForDay"] = 1;
            }
            else
            {
                ViewData["LimitForDay"] = 0;
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


        public IActionResult Open(int id)
        {
            Task task = taskDAL.Reed(id);

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
            IEnumerable<Schedule> ss = scheduleDAL.Reed().Where(s => s.Task_id == id);
            foreach (var item in ss)
            {
                scheduleDAL.Delete(item.Id);
            }

            return RedirectToAction("Main");

        }

        [HttpPost]
        public IActionResult EditWork([Bind] Task task)
        {

            Task tasknew = taskDAL.Reed(task.Id);
            tasknew.Description = task.Description;

            tasknew.Progress += 10;
            taskDAL.Update(tasknew);

            return RedirectToAction("Main");

        }


        public IActionResult Maked(int id = 0)
        {
            Schedule s = scheduleDAL.Reed().Where(s => s.Schedule_date.Date == DateTime.Now.Date)
                .First(s => s.Task_id == id);

            if (s.Make == 0) { s.Make = 1; }
            else { s.Make = 0; }

            scheduleDAL.Update(s);

            ViewData["Maked"] = s.Make;

            return RedirectToAction("Main");

        }


        public IActionResult AddToScheduler(int id = 0)
        {
            ////drop maked and past tasks

            IList<Schedule> schedules = scheduleDAL.Reed().ToList();

            if (schedules.Count < 3 && schedules.Where(s => s.Task_id == id).Count() == 0)
            {

                Schedule schedule = new Schedule();
                schedule.Task_id = id;
                schedule.Schedule_date = DateTime.Now;

                int b = scheduleDAL.Create(schedule);

                
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

            ViewData["LimitForDay"] = 0;

            return RedirectToAction("Main");
        }

        [HttpPost]
        public IActionResult Sinhronize([Bind] Planner.Models.CalendarId calendarId)
        {

            IEnumerable<Task> tasks = PlannerLib.workLoggic.GoogleSync.GetTasksFromGoogleCalendar(calendarId.Id);

            IEnumerable<Task> localTasks = taskDAL.Reed();

            //находим совпадения и исключаем их
            IEnumerable<string> originalTasksNames = tasks.Select(x => x.Name).Except(localTasks.Select(y => y.Name)).ToList();


            foreach (var item in originalTasksNames)
            {
                taskDAL.Create(tasks.First(t => t.Name == item));
            }

            //проверка на отсутствие тех тасков что есть в локальных, но нет в подтянутых с конкретным лейблом
            localTasks = localTasks.Where(lt => lt.Label == calendarId.Id);

            IEnumerable<string> originalTasksNamesPast =
                localTasks.Select(x => x.Name).Except(tasks.Select(y => y.Name)).ToList();

            IEnumerable<Schedule> s = scheduleDAL.Reed();

            foreach (var item in originalTasksNamesPast)
            {
                var t = localTasks.First(t => t.Name == item);

                var sss = s.FirstOrDefault(ss => ss.Task_id == t.Id);
                if (sss != null)
                {
                    scheduleDAL.Delete(s.FirstOrDefault(ss => ss.Task_id == t.Id).Id);
                }

                taskDAL.Delete(t.Id);
            }


            return RedirectToAction("Main");
        }




    }
}
