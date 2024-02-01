using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAffectedByColumnOnConfigurableEntityColumnDefintion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AffectedBy",
                table: "ConfigurableEntityColumnDefinitions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AffectedBy",
                table: "ConfigurableEntityColumnDefinitions",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
