using PlannerLibrary.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLibrary.db.ModelDbControllers
{
    public class UserDbController : IOperations<User>
    {

        public UserDbController()
        {
            dbController = new DbController<User>();
        }


        DbController<User> dbController;

        public int Add(User obj)
        {
            return dbController.Add(obj);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Reed(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> Reed()
        {
            throw new NotImplementedException();
        }

        public void Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
