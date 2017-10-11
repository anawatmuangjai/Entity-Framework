using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System.Data.Entity;

namespace CrudApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            //InsertNinja();
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            //DeleteNinja();
            //InsertNinjaWithEquipment();
            ProjectionQuery();
            Console.ReadKey();
        } 

        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //Select all
                var ninja = context.Ninjas
                    .Select(n => new 
                    { 
                        n.Name, 
                        n.DateOfBrith, 
                        n.EquipmentOwned 
                    })
                    .ToList();
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = new Ninja
                {
                    Name = "Kecy Catanzero",
                    ServedInOniwaban = false,
                    DateOfBrith = new DateTime(1990, 1, 14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var spunk = new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                };

                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.SaveChanges();
            }
        }

        private static void DeleteNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                //Lamda syntex read data
                //Ex.1
                //var ninjas = context.Ninjas.Where(n => n.Name == "AnawatSan");
                //var ninjas = context.Ninjas
                //    .Where(n => n.DateOfBrith >= new DateTime(1984, 1, 1))
                //foreach(var ninja in ninjas)
                //{
                //    Console.WriteLine(ninja.Name + " | " + ninja.DateOfBrith.ToLongDateString());
                //}

                //Ex.2 select top
                var ninja = context.Ninjas
                    .Where(n => n.DateOfBrith >= new DateTime(1984, 1, 1))
                    .FirstOrDefault();

                Console.WriteLine(ninja.Name + " | " + ninja.DateOfBrith.ToLongDateString());
            }
        }

        private static void InsertNinja()
        {
            var ninja1 = new Ninja 
            {
                Name = "Leonardo",
                ServedInOniwaban =false,
                DateOfBrith = new DateTime(1984,1,1), //(Y,M,D)
                ClanId = 1
            };

            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBrith = new DateTime(1985, 1, 1), //(Y,M,D)
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //context.Ninjas.Add(ninja1);
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }
        }
    }
}
