using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RescueSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixmedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SubmittedTime",
                table: "Requests");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "RequestMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SecureUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PublicId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestMedia_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestMedia_RequestId",
                table: "RequestMedia",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestMedia");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Requests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedTime",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }
    }
}
