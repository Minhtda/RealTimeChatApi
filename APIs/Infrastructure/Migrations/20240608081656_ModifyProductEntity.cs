using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyProductEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Posts_PostId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PostId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProductId",
                table: "Posts",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Products_ProductId",
                table: "Posts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Products_ProductId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ProductId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PostId",
                table: "Products",
                column: "PostId",
                unique: true,
                filter: "[PostId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Posts_PostId",
                table: "Products",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
