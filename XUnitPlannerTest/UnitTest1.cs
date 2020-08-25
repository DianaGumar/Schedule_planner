using System;
using Xunit;

using PlannerLibrary.Model;
using PlannerLib.DataBase;
using System.Collections.Generic;

namespace XUnitPlannerTest
{
    public class UnitTest1
    {
        [Fact]
        public void addUser()
        {
            User user = new User("ffhdrhr", "1111", "fdbfdb");
            var userController = new MysqlController<User>();

            int i = userController.Create(user);

            Assert.Equal(1, i);
        }

        [Fact]
        public void ReadUser()
        {
            var userController = new MysqlController<User>();

            User user2 = userController.Reed(4);

            Assert.NotNull(user2);
        }

        [Fact]
        public void UpdateUser()
        {
            var userController = new MysqlController<User>();

            User user = userController.Reed(2);
            user.name = "222";

            int i = userController.Update(user);

            Assert.Equal(1, i);
        }


        [Fact]
        public void DeleteUser()
        {
            var userController = new MysqlController<User>();

            int i = userController.Delete(1);

            Assert.Equal(1, i);
        }

        [Fact]
        public void ReadAllUser()
        {
            var userController = new MysqlController<User>();

            List<User> users = (List<User>)userController.Reed();

            Assert.NotEmpty(users);
        }


    }
}
