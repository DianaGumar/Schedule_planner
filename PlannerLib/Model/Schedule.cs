using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.Model
{
    public class Schedule
    {

        public Schedule() { }

        public int Id { get; set; }

        public int Task_id { get; set; }

        public DateTime Schedule_date { get; set; }

        public int Make { get; set; }

        

    }
}
