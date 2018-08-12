using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new PtContext())
            {
                context.Database.Migrate();

                SetupBasisSchedule(context);
                CreateMuscleGroups(context);
                context.SaveChanges();
            }
        }

        public static void SetupBasisSchedule(PtContext context)
        {
            var cycling = new Workout {Name = "Cycling"};
            var fitness1 = new Workout {Name = "Shoulders, legs, abs"};
            var fitness2 = new Workout {Name = "Chest, triceps"};
            var fitness3 = new Workout {Name = "Back, biceps, abs"};

            var now = DateTime.Now;
            var year = now.Year;
            var month = now.Month;
            var today = now.Day;

            var user = new User
            {
                Name = "Ronald",
                Trainings = new List<Training>
                {
                    Add(new DateTime(year, month, today - 7), fitness1, true),
                    Add(new DateTime(year, month, today - 6), fitness2, true),
                    Add(new DateTime(year, month, today - 4), fitness3, true),
                    Add(new DateTime(year, month, today - 3), cycling),

                    Add(new DateTime(year, month, today - 1), fitness1),
                    Add(new DateTime(year, month, today), fitness2),
                    Add(new DateTime(year, month, today + 2), fitness3),
                    Add(new DateTime(year, month, today + 3), cycling),
                }
            };
            context.Users.Add(user);
        }

        private static void CreateMuscleGroups(PtContext context)
        {
            context.MuscleGroups.AddRange(new List<MuscleGroup>
            {
                new MuscleGroup {Name = "Legs"},
                new MuscleGroup {Name = "Abs"},
                new MuscleGroup {Name = "Chest"},
                new MuscleGroup {Name = "Back"},
                new MuscleGroup {Name = "Biceps"},
                new MuscleGroup {Name = "Triceps"},
                new MuscleGroup {Name = "Shoulders"},
                new MuscleGroup {Name = "Forearms"}
            });
        }

        private static Training Add(DateTime dateTime, Workout workout, bool finished = false)
        {
            return new Training
            {
                Date = dateTime,
                Workout = workout,
                Finished = finished
            };
        }
    }
}