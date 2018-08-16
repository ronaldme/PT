using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PT.DAL;
using PT.DAL.Entities;

namespace PT.Setup
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PtContext())
            {
                db.Database.Migrate();

                CreateMuscleGroups(db);
                SetupBasisSchedule(db);

                db.SaveChanges();
            }
        }

        public static void SetupBasisSchedule(PtContext db)
        {
            var cycling = new WorkoutType {Name = "Cycling"};
            var fitness1 = new WorkoutType {Name = "Shoulders, legs, abs"};
            var fitness2 = new WorkoutType {Name = "Chest, triceps"};
            var fitness3 = new WorkoutType {Name = "Back, biceps, abs"};

            fitness2.ExerciseWorkoutType = new List<ExerciseWorkoutType>
            {
                new ExerciseWorkoutType
                {
                    Exercise = new Exercise {Name = "Bench press"},
                    WorkoutType = fitness2
                }
            };

            var now = DateTime.Now;
            var year = now.Year;
            var month = now.Month;
            var today = now.Day;

            var user = new User
            {
                Name = "Ronald",
                Workouts = new List<Workout>
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
            db.Users.Add(user);
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

        private static Workout Add(DateTime dateTime, WorkoutType workoutType, bool finished = false)
        {
            return new Workout
            {
                Date = dateTime,
                WorkoutType = workoutType,
                Finished = finished
            };
        }
    }
}