using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFiltersColumnOnConfigurableEntityColumnDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE ConfigurableEntityColumnDefinitions ADD COLUMN FieldFormatter nvarchar(100) DEFAULT null AFTER DisplayName");
            migrationBuilder.Sql("UPDATE ConfigurableEntityColumnDefinitions SET FieldFormatter = 'MM/DD/YYYY' WHERE FieldFormatter IS NULL and (ColumnName = 'CreatedAt')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE ConfigurableEntityColumnDefinitions DROP COLUMN FieldFormatter");
        }
    }
}
