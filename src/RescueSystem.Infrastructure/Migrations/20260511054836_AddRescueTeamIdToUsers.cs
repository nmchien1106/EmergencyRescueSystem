using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace RescueSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRescueTeamIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RescueTeamId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RescueTeamId",
                table: "Users",
                column: "RescueTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RescueTeams_RescueTeamId",
                table: "Users",
                column: "RescueTeamId",
                principalTable: "RescueTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RescueTeams_RescueTeamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RescueTeamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RescueTeamId",
                table: "Users");
        }
    }
}
