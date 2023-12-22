using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.InsertData("AspNetRoles", new[] { "Id", "Name", "NormalizedName" }, new object[,]
            {
                { Guid.NewGuid().ToString(), "Admin", "ADMIN" },
                { Guid.NewGuid().ToString(), "IT", "IT" },
                { Guid.NewGuid().ToString(), "Client", "CLIENT" }
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("AspNetRoles", "Id", new object[] { "Admin" });
            migrationBuilder.DeleteData("AspNetRoles", "Id", new object[] { "IT" });
            migrationBuilder.DeleteData("AspNetRoles", "Id", new object[] { "Client" });
        }
    }
}
