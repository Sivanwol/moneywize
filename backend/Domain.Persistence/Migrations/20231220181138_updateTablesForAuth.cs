using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateTablesForAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLog",
                table: "ActivityLog");

            migrationBuilder.RenameTable(
                name: "ActivityLog",
                newName: "ActivityLogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ActivityLogs",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                table: "ActivityLogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLogs_AspNetUsers_UserId",
                table: "ActivityLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityLogs",
                table: "ActivityLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLogs_UserId",
                table: "ActivityLogs");

            migrationBuilder.RenameTable(
                name: "ActivityLogs",
                newName: "ActivityLog");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ActivityLog",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityLog",
                table: "ActivityLog",
                column: "Id");
        }
    }
}
