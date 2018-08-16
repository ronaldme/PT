namespace PT.DAL.Entities
{
    public class ExerciseWorkoutType
    {
        public virtual int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
        public virtual int WorkoutTypeId { get; set; }
        public virtual WorkoutType WorkoutType { get; set; }
    }
}