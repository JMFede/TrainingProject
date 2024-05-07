using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingProject.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "lineId", "lineName" },
                values: new object[,]
                {
                    { 1, "Packing" },
                    { 2, "Machining" },
                    { 3, "Assembly" },
                    { 4, "Fabrication" },
                    { 5, "Production" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "statusId", "statusName" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "Closed" },
                    { 3, "Planned" },
                    { 4, "Paused" }
                });

            migrationBuilder.InsertData(
                table: "Types",
                columns: new[] { "typeId", "typeName" },
                values: new object[,]
                {
                    { 1, "PR001" },
                    { 2, "PR002" },
                    { 3, "PR003" },
                    { 4, "PR004" },
                    { 5, "PR005" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "userId", "password", "userName" },
                values: new object[,]
                {
                    { 1, null, "User1" },
                    { 2, null, "User2" },
                    { 3, null, "User3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "lineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "lineId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "lineId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "lineId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Lines",
                keyColumn: "lineId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "statusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "statusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "statusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderStatus",
                keyColumn: "statusId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "typeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "typeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "typeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "typeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Types",
                keyColumn: "typeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "userId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "userId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "userId",
                keyValue: 3);
        }
    }
}
