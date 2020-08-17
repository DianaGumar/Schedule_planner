using System;
using Xunit;

using PlannerLibrary.db.ModelDbControllers;
using PlannerLibrary.Model;

namespace XUnitPlannerTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            User user = new User("root", "1111", "lantan.mp4@gmail.com");

            UserDbController userDbController = new UserDbController();

            int i = userDbController.Add(user);

            Assert.Equal(1, i);


        }
    }
}
