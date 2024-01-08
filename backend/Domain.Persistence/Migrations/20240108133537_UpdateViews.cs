using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurableEntityColumnDefinitions_ConfigurableCategories_C~",
                table: "ConfigurableEntityColumnDefinitions");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "ConfigurableUserViews");

            migrationBuilder.DropColumn(
                name: "LastUsed",
                table: "ConfigurableUserViews");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "ConfigurableEntityColumnDefinitions");

            migrationBuilder.RenameColumn(
                name: "IsPinable",
                table: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions",
                newName: "IsHidden");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ConfigurableUserViews",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ConfigurableUserViews",
                type: "varchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Permissions",
                table: "ConfigurableUserViews",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsFixed",
                table: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ConfigurableEntityColumnDefinitions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ConfigurableEntityColumnDefinitions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurableEntityColumnDefinitions_ConfigurableCategories_C~",
                table: "ConfigurableEntityColumnDefinitions",
                column: "CategoryId",
                principalTable: "ConfigurableCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurableEntityColumnDefinitions_ConfigurableCategories_C~",
                table: "ConfigurableEntityColumnDefinitions");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "ConfigurableUserViews");

            migrationBuilder.DropColumn(
                name: "IsFixed",
                table: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions");

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                table: "ConfigurableUserViewHasConfigurableEntityColumnDefinitions",
                newName: "IsPinable");

            migrationBuilder.UpdateData(
                table: "ConfigurableUserViews",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ConfigurableUserViews",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "ConfigurableUserViews",
                keyColumn: "Color",
                keyValue: null,
                column: "Color",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ConfigurableUserViews",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "ConfigurableUserViews",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsed",
                table: "ConfigurableUserViews",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ConfigurableEntityColumnDefinitions",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ConfigurableEntityColumnDefinitions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ConfigurableEntityColumnDefinitions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "ConfigurableEntityColumnDefinitions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurableEntityColumnDefinitions_ConfigurableCategories_C~",
                table: "ConfigurableEntityColumnDefinitions",
                column: "CategoryId",
                principalTable: "ConfigurableCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
