using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.Model
{
    public class User
    {

        public User() { Role = 1; }

        //public User(string name, string password, string email, string phone = null)
        //{
        //    id = 0;
        //    this.name = name;
        //    this.password = password;
        //    this.email = email;
        //    this.phone = phone;
        //}


        //public User(int id, string name, string password, string email, string phone = null)
        //{
        //    this.id = id;
        //    this.name = name;
        //    this.password = password;
        //    this.email = email;
        //    this.phone = phone;
        //}

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Role { get; set; }

        public override string ToString()
        {
            return Id + " " + Name + " " + Password + " " + Email + " " + Phone;
        }

    }
}
