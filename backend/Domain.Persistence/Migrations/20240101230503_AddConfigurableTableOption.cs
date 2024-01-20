using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurableTableOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flag",
                table: "Languages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConfigurableCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    Icon = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurableCategories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConfigurableEntityColumnDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EntityName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FieldAlias = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FieldPath = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    FilterOperationType = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    IsSortable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsFilterable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPublic = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DisabledAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Permissions = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsVirtualColumn = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    MetaData = table.Column<string>(type: "json", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AffectedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurableEntityColumnDefinitions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableEntityColumnDefinitions_CategoryId",
                table: "ConfigurableEntityColumnDefinitions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableEntityColumnDefinitions_EntityName_ColumnName_Di~",
                table: "ConfigurableEntityColumnDefinitions",
                columns: new[] { "EntityName", "ColumnName", "FieldAlias" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurableEntityColumnDefinitions");

            migrationBuilder.DropTable(
                name: "ConfigurableCategories");

            migrationBuilder.DropColumn(
                name: "Flag",
                table: "Languages");
        }
    }
}
