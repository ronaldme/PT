using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Setup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CreateSampleWorkoutCalenderItems();
        }

        private static async Task CreateSampleWorkoutCalenderItems()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PtDbContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=PT;User Id=guest;Password=guest");

            await using var db = new PtDbContext(optionsBuilder.Options);
            await DropAndMigrate(db);

            var workout = CreateWorkout();
            db.Workouts.Add(workout);

            CreateWorkoutCalenderItems(db, workout);

            await db.SaveChangesAsync();
        }

        private static async Task DropAndMigrate(PtDbContext db)
        {
            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        private static void CreateWorkoutCalenderItems(PtDbContext db, Workout workout)
        {
            var day = DateTimeOffset.Now.Date;
            for (int i = 0; i < 100; i++)
            {
                db.WorkoutCalenderItem.Add(new WorkoutCalenderItem
                {
                    Workout = workout,
                    Date = day
                });

                day = day.AddDays(1);
            }
        }

        private static Workout CreateWorkout()
            => new Workout
            {
                Name = "One punch man",
            };
    }
}
