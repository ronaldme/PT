using Microsoft.EntityFrameworkCore.Migrations;

namespace PT.DAL.Migrations
{
    public partial class Restrict_Deletion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutCalenderItem_Workouts_WorkoutId",
                table: "WorkoutCalenderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutCalenderItem_Workouts_WorkoutId",
                table: "WorkoutCalenderItem",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutCalenderItem_Workouts_WorkoutId",
                table: "WorkoutCalenderItem");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutCalenderItem_Workouts_WorkoutId",
                table: "WorkoutCalenderItem",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
