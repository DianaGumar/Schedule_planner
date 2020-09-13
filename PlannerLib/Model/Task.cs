using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.Model
{ 

    public enum Priority { Urgent , High, Medium, Low }
    public enum TimeVolume { less_then_1_hours, less_then_3_hours, more_then_3_hours }


    public class Task
    {
        public Task() { }

        private string name;
        private string description;
        private string label;
        private DateTime? deadline;
        private int progress;


        public int Id { get; set; }

        public string Name { set { if (value.Length < 100) name = value; } get { return name; } }
        
        public string Description { set { if (value != null && value.Length < 400) description = value; } get { return description; } }

        public string Label { set { if (value != null && value.Length < 100) label = value; } get { return label; } } 

        public int Priority { get; set; }

        public DateTime? Deadline { set { if (value > DateTime.Now) deadline = value; } get { return deadline; } }

        public int Time_volume { get; set; }

        public int Progress { get { return progress; } set { if (value < 100) progress = value; } }

    }
}
