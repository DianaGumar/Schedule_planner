using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.Model
{
    public class Task
    {
        public Task() { }

        private string name;
        private string description;
        private string label;
        private DateTime deadline;


        public int Id { get; set; }

        public string Name { set { if (value.Length < 100) name = value; } get { return name; } }
        
        public string Description { set { if (value != null && value.Length < 400) description = value; } get { return description; } }

        public string Label { set { if (value != null && value.Length < 100) label = value; } get { return label; } } 

        public int Priority { get; set; }

        public DateTime Deadline { set { if (value > DateTime.Now) deadline = value; } get { return deadline; } }

        public int Time_volume { get; set; }

        public int Progress { get; set; }

    }
}
