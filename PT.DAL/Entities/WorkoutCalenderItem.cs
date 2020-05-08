using System;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    public class WorkoutCalenderItem : Entity
    {
        public DateTimeOffset Date { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        public bool IsCompleted { get; set; }

        public string Remark { get; set; }
    }
}