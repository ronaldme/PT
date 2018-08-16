using System.Collections.Generic;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    // One exercise e.g.:
    // Deadlift
    // Bench press
    // Bicep curls
    // Running
    // Cycling
    // Walking
    // Swiming
    public class Exercise : Entity
    {
        public virtual string Name { get; set; }
        public virtual ICollection<MuscleGroup> MuscleGroups { get; set; }
        public virtual ICollection<ExerciseWorkoutType> ExerciseWorkoutType { get; set; }
    }
}