using System.Collections.Generic;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    // A WorkoutType is an event e.g. gym for 1 hour which consist of multiple exercises
    // or running, cycling, swimming which only contains 1 exercise.
    public class WorkoutType : Entity
    {
        public virtual string Name { get; set; }
        public virtual ICollection<ExerciseWorkoutType> ExerciseWorkoutType { get; set; }
    }
}