using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureableViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "ConfigurableEntityColumnDefinitions");

            migrationBuilder.CreateTable(
                name: "ConfigurableUserViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsShareAble = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Color = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Settings = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPredefined = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastUsed = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurableUserViews", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConfigurableUserViewTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurableUserViewTags", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions",
                columns: table => new
                {
                    ConfigurableUserViewId = table.Column<int>(type: "int", nullable: false),
                    ConfigurableEntityColumnDefinitionId = table.Column<int>(type: "int", nullable: false),
                    IsPinable = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurableUserViewHasConfigurableEntityColumnDefinitions", x => new { x.ConfigurableUserViewId, x.ConfigurableEntityColumnDefinitionId });
                    
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConfigurableUserViewHasConfigurableUserViewTags",
                columns: table => new
                {
                    ConfigurableUserViewId = table.Column<int>(type: "int", nullable: false),
                    ConfigurableUserViewTagsId = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurableUserViewHasConfigurableUserViewTags", x => new { x.ConfigurableUserViewId, x.ConfigurableUserViewTagsId });
                    
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViewHasConfigurableEntityColumnDefinitions_C~",
                table: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions",
                column: "ConfigurableEntityColumnDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViewHasConfigurableUserViewTags_Configurable~",
                table: "ConfigurableUserViewHasConfigurableUserViewTags",
                column: "ConfigurableUserViewTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViews_UserId_Name",
                table: "ConfigurableUserViews",
                columns: new[] { "UserId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViewTags_UserId_Name",
                table: "ConfigurableUserViewTags",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions");

            migrationBuilder.DropTable(
                name: "ConfigurableUserViewHasConfigurableUserViewTags");

            migrationBuilder.DropTable(
                name: "ConfigurableUserViews");

            migrationBuilder.DropTable(
                name: "ConfigurableUserViewTags");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "ConfigurableEntityColumnDefinitions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
