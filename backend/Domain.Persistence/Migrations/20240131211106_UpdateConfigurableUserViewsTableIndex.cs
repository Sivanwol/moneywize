using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurableUserViewsTableIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfigurableUserViews_ClientUniqueKey",
                table: "ConfigurableUserViews");
            migrationBuilder.DropIndex(
                name: "IX_ConfigurableUserViews_UserId_Name",
                table: "ConfigurableUserViews");
            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViews_UserId_ClientUniqueKey_Name",
                table: "ConfigurableUserViews",
                columns: new[] { "UserId", "ClientUniqueKey", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfigurableUserViews_UserId_ClientUniqueKey_Name",
                table: "ConfigurableUserViews");
            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViews_ClientUniqueKey",
                table: "ConfigurableUserViews",
                columns: new[] { "ClientUniqueKey" },
                unique: true);
            
            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViews_UserId_Name",
                table: "ConfigurableUserViews",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }
    }
}
