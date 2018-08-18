using System;
using System.Collections.Generic;

namespace PT.Web.Features.Workouts
{
    public class AddEditWorkoutViewModel
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public int SelectedWorkoutType { get; set; }
        public Dictionary<int, string> WorkoutTypes { get; set; }
    }
}