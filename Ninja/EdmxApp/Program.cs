using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdmxApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var ninjaContext = new NinjaEntities();

            var ninjas = ninjaContext.Ninjas
                .Where(n => n.DateOfBrith >= new DateTime(1984, 1, 1));

            foreach(var ninja in ninjas)
            {
                Console.WriteLine(ninja.Name + " | " + ninja.DateOfBrith.ToLongDateString());
            }

            Console.ReadKey();
        }
    }
}
