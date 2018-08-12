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

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
    }
}