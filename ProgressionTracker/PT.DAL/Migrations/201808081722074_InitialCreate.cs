namespace PT.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MuscleGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Exercise_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id)
                .Index(t => t.Exercise_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Finished = c.Boolean(nullable: false),
                        Workout_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workouts", t => t.Workout_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Workout_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Workout_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workouts", t => t.Workout_Id)
                .Index(t => t.Workout_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Trainings", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.Exercises", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.MuscleGroups", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.Exercises", new[] { "Workout_Id" });
            DropIndex("dbo.Trainings", new[] { "User_Id" });
            DropIndex("dbo.Trainings", new[] { "Workout_Id" });
            DropIndex("dbo.MuscleGroups", new[] { "Exercise_Id" });
            DropTable("dbo.Exercises");
            DropTable("dbo.Workouts");
            DropTable("dbo.Trainings");
            DropTable("dbo.Users");
            DropTable("dbo.MuscleGroups");
        }
    }
}
