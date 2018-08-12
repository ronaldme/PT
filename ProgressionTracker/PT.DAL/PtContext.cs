using Microsoft.EntityFrameworkCore;
using PT.DAL.Entities;

namespace PT.DAL
{
    /// <summary>
    /// Add-Migration NAME -ProjectName PT.DAL -StartUpProjectName PT.Startup
    /// Update-database (apply migrations)
    /// </summary>
    public class PtContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(local);Database=PT.Database;Integrated Security=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
    }
}