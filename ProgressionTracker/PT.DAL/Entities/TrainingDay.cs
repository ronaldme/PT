using System;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class TrainingDay : Entity
    {
        public virtual DayOfWeek Day { get; set; }
        public virtual Workout Workout { get; set; }
    }
}