using System.Collections.Generic;
using PT.DAL.Infrastructure;

namespace PT.DAL.Entities
{
    // Workout is an event e.g. gym for 1 hour which consist of multiple exercises
    // or running, cycling, swimming which only contains 1 exercise.
    public class Workout : Entity
    {
        public virtual string Name { get; set; }
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}