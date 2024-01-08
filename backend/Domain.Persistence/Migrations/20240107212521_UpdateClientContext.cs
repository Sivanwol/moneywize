using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ClientContacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "ClientContacts",
                type: "varchar(100)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContacts_ClientId",
                table: "ClientContacts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContacts_FirstName_LastName_ClientId_CountryId",
                table: "ClientContacts",
                columns: new[] { "FirstName", "LastName", "ClientId", "CountryId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContacts_Clients_ClientId",
                table: "ClientContacts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientContacts_Clients_ClientId",
                table: "ClientContacts");

            migrationBuilder.DropIndex(
                name: "IX_ClientContacts_ClientId",
                table: "ClientContacts");

            migrationBuilder.DropIndex(
                name: "IX_ClientContacts_FirstName_LastName_ClientId_CountryId",
                table: "ClientContacts");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientContacts");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "ClientContacts");
        }
    }
}
