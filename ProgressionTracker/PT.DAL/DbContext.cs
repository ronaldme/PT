using System.Data.Entity;
using PT.DAL.Entities;

namespace PT.DAL
{
    /// <summary>
    /// Add-Migration NAME -ProjectName PT.DAL -StartUpProjectName PT.Startup
    /// </summary>
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, Migrations.Configuration>(true));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
    }
}