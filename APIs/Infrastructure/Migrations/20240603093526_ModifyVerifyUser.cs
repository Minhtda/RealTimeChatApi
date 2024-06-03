using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyVerifyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerifyUser_Users_UserId",
                table: "VerifyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerifyUser",
                table: "VerifyUser");

            migrationBuilder.DropIndex(
                name: "IX_VerifyUser_UserId",
                table: "VerifyUser");

            migrationBuilder.RenameTable(
                name: "VerifyUser",
                newName: "VerifyUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "VerifyUserId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerifyUsers",
                table: "VerifyUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VerifyUsers_UserId",
                table: "VerifyUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VerifyUsers_Users_UserId",
                table: "VerifyUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerifyUsers_Users_UserId",
                table: "VerifyUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerifyUsers",
                table: "VerifyUsers");

            migrationBuilder.DropIndex(
                name: "IX_VerifyUsers_UserId",
                table: "VerifyUsers");

            migrationBuilder.DropColumn(
                name: "VerifyUserId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "VerifyUsers",
                newName: "VerifyUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerifyUser",
                table: "VerifyUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_VerifyUser_UserId",
                table: "VerifyUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerifyUser_Users_UserId",
                table: "VerifyUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
