using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addClientUniqueKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            var sql = @"ALTER TABLE ConfigurableUserViews ADD COLUMN ClientUniqueKey varchar(36) NULL AFTER Id;
                UPDATE ConfigurableUserViews SET ClientUniqueKey = uuid() WHERE ClientUniqueKey IS NULL;";
            migrationBuilder.Sql(sql);
            migrationBuilder.CreateIndex(
                name: "IX_ConfigurableUserViews_ClientUniqueKey",
                table: "ConfigurableUserViews",
                column: "ClientUniqueKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ConfigurableUserViews_ClientUniqueKey",
                table: "ConfigurableUserViews");
            migrationBuilder.DropColumn(
                name: "ClientUniqueKey",
                table: "ConfigurableUserViews");
        }
    }
}
