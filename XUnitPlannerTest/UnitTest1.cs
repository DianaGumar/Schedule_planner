using System;
using Xunit;

using PlannerLib.DataBase;
using System.Collections.Generic;
using PlannerLib.Model;

namespace XUnitPlannerTest
{
    public class UnitTest1
    {
        [Fact]
        public void addUser()
        {
            User user = new User("root", "1111", "aaa@mail.ru");
            var userController = new MysqlController<User>();

            int i = userController.Create(user);

            Assert.Equal(1, i);
        }

        [Fact]
        public void ReadUser()
        {
            var userController = new MysqlController<User>();

            User user2 = userController.Reed(2);

            Assert.NotNull(user2);
        }

        [Fact]
        public void UpdateUser()
        {
            var userController = new MysqlController<User>();

            User user = userController.Reed(2);
            user.Name = "222";

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


        [Fact]
        public void ReadUserBy()
        {
            var userController = new MysqlController<User>();

            User u = new User();
            u.Password = "1234";
            u.email = "lantan.mp4@gmail.com";

            u = userController.Reed(u, "Email", "Password");

            Assert.NotNull(u);
        }


    }
}
