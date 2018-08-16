using Microsoft.EntityFrameworkCore;
using PT.DAL.Entities;

namespace PT.DAL
{
    /// <summary>
    /// add-migration
    /// update-database
    /// </summary>
    public class PtContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=PT.Database;Integrated Security=True;MultipleActiveResultSets=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseWorkoutType>()
                .HasKey(t => new { t.ExerciseId, t.WorkoutTypeId });

            modelBuilder.Entity<ExerciseWorkoutType>()
                .HasOne(pt => pt.Exercise)
                .WithMany(p => p.ExerciseWorkoutType)
                .HasForeignKey(pt => pt.ExerciseId);

            modelBuilder.Entity<ExerciseWorkoutType>()
                .HasOne(pt => pt.WorkoutType)
                .WithMany(t => t.ExerciseWorkoutType)
                .HasForeignKey(pt => pt.WorkoutTypeId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutType> WorkoutTypes { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
    }
}