using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLibrary.Model
{
    public class User
    {

        public User() { }

        public User(string name, string password, string email, string phone = null)
        {
            id = 0;
            this.name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
        }

        internal int id;
        internal string name;
        internal string password;
        internal string email;
        internal string phone;
        internal int role = 1;



    }
}
