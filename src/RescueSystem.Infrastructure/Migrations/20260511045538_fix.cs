using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RescueSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "RescueTeams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RescueTeams_ApplicationUserId",
                table: "RescueTeams",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RescueTeams_Users_ApplicationUserId",
                table: "RescueTeams",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RescueTeams_Users_ApplicationUserId",
                table: "RescueTeams");

            migrationBuilder.DropIndex(
                name: "IX_RescueTeams_ApplicationUserId",
                table: "RescueTeams");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "RescueTeams");
        }
    }
}
