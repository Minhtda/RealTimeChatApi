using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToExchangeCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExchangeConditions",
                columns: new[] { "ConditionId", "ConditionType" },
                values: new object[,]
                {
                    { 1, "For exchanging" },
                    { 2, "For donation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExchangeConditions",
                keyColumn: "ConditionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExchangeConditions",
                keyColumn: "ConditionId",
                keyValue: 2);
        }
    }
}
