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
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetRoles_RoleId1`;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetUsers_UserId1`;";
            sql += "DROP INDEX IF EXISTS `FK_AspNetUserRoles_AspNetRoles_RoleId` ON `AspNetUserRoles`;";
            sql += "DROP INDEX IF EXISTS `FK_AspNetUserRoles_AspNetUsers_UserId` ON `AspNetUserRoles`;";
            migrationBuilder.Sql(sql);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.Sql("SET FOREIGN_KEY_CHECKS = 1;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "SET FOREIGN_KEY_CHECKS = 0;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetRoles_RoleId1`;";
            sql += "ALTER TABLE `AspNetUserRoles` DROP FOREIGN KEY `FK_AspNetUserRoles_AspNetUsers_UserId1`;";
            sql += "DROP INDEX IF EXISTS `FK_AspNetUserRoles_AspNetRoles_RoleId1` ON `AspNetUserRoles`;";
            sql += "DROP INDEX IF EXISTS `FK_AspNetUserRoles_AspNetUsers_UserId1` ON `AspNetUserRoles`;";
            migrationBuilder.Sql(sql);
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");
        }
    }
}
