using System;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class PlannedWorkout : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual Workout Workout { get; set; }
        public virtual bool Finished { get; set; }
    }
}