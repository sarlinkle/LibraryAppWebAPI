using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAppWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_Books_BookId",
                table: "Checkouts");

            migrationBuilder.DropIndex(
                name: "IX_Checkouts_BookId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Checkouts");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryCardNumber",
                table: "LibraryUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDue",
                table: "Checkouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CheckoutId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CheckoutId",
                table: "Books",
                column: "CheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Checkouts_CheckoutId",
                table: "Books",
                column: "CheckoutId",
                principalTable: "Checkouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Checkouts_CheckoutId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CheckoutId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateDue",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "CheckoutId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "LibraryCardNumber",
                table: "LibraryUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Checkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_BookId",
                table: "Checkouts",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_Books_BookId",
                table: "Checkouts",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
