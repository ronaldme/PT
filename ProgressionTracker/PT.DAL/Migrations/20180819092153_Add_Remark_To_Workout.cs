using Microsoft.EntityFrameworkCore.Migrations;

namespace PT.DAL.Migrations
{
    public partial class Add_Remark_To_Workout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Workouts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Workouts");
        }
    }
}
