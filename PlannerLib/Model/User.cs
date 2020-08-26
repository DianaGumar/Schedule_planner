using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.Model
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


        public User(int id, string name, string password, string email, string phone = null)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.phone = phone;
        }

        public int Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Phone { get { return phone; } set { phone = value; } }


        public int id;
        public string name;
        public string password;
        public string email;
        public string phone;
        public int role = 1;

        public override string ToString()
        {
            return id + " " + name + " " + password + " " + email + " " + phone;
        }

    }
}
