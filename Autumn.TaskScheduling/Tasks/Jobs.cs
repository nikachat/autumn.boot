using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autumn.TaskScheduling.Tasks
{
    public class Jobs
    {
        public static async Task Todo1()
        {
            // to do...
            await new Task(() => { });
            Console.WriteLine("Task Jobs Todo1");
        }

        public static async Task Todo2()
        {
            // to do...
            await new Task(() => { });
            Console.WriteLine("Task Jobs Todo2");
        }

        public static async Task Todo3()
        {
            // to do...
            await new Task(() => { });
            Console.WriteLine("Task Jobs Todo3");
        }

        public static async Task Todo4()
        {
            // to do...
            await new Task(()=> { });
            Console.WriteLine("Task Jobs Todo4");
        }
    }
}
