using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveUserRoleToExtrnalEnitity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            var sql = "SET FOREIGN_KEY_CHECKS = 0;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetRoles_RoleId`;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetUsers_UserId`;";
            migrationBuilder.Sql(sql);


            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "SET FOREIGN_KEY_CHECKS = 0;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetRoles_RoleId`;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetUsers_UserId`;";
            migrationBuilder.Sql(sql);
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
        }
    }
}
