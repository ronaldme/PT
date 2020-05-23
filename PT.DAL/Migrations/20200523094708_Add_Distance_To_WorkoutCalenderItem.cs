using Microsoft.EntityFrameworkCore.Migrations;

namespace PT.DAL.Migrations
{
    public partial class Add_Distance_To_WorkoutCalenderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Distance",
                table: "WorkoutCalenderItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "WorkoutCalenderItem");
        }
    }
}
