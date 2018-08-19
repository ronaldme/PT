using System;
using PT.Web.Features.WorkoutsType;

namespace PT.Web.Features.Workouts
{
    public class WorkoutViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public WorkoutTypeViewModel WorkoutType { get;set; }
        public string Remark { get; set; }
        public bool Finished { get; set; }
    }
}