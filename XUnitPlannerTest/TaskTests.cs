using PlannerLib.DataBase;
using PlannerLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitPlannerTest
{
    public class TaskTests
    {
        [Fact]
        public void addTask()
        {
            Task obj = new Task();

            obj.Name = "hiiiii";
            obj.Description = "dddwd dsdefe ryruol, k";
            obj.Label = "university";
            obj.Priority = 0;
            obj.Time_volume = 5;


            var controller = new MysqlController<Task>();

            int i = controller.Create(obj);

            Assert.Equal(1, i);
        }


    }
}
