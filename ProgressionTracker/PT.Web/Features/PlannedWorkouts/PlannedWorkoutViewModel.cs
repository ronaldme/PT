using System;
using PT.Web.Features.Workouts;

namespace PT.Web.Features.PlannedWorkouts
{
    public class PlannedWorkoutViewModel
    {
        public DateTime Date { get; set; }
        public WorkoutViewModel Workout { get;set; }
        public bool Finished { get; set; }
    }
}