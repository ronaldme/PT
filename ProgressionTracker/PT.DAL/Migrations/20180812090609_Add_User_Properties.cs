using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PT.DAL.Migrations
{
    public partial class Add_User_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AspNetUsersId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TelegramChatId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TelegramChatId",
                table: "Users");
        }
    }
}