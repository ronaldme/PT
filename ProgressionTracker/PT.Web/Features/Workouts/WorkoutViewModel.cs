using System;
using PT.Web.Features.WorkoutsType;

namespace PT.Web.Features.Workouts
{
    public class WorkoutViewModel
    {
        public DateTime Date { get; set; }
        public WorkoutTypeViewModel WorkoutType { get;set; }
        public bool Finished { get; set; }
        public int Id { get; set; }
    }
}