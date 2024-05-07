using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingProject.Migrations
{
    /// <inheritdoc />
    public partial class InsertExampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WBS",
                table: "Orders",
                newName: "Wbs");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "orderId", "batch", "lineId", "name", "plannedDate", "plannedQuantity", "quantity", "startingDate", "statusId", "typeId", "userId", "Wbs" },
                values: new object[,]
                {
                    { 1, "BATCH001", 1, "Order 1", "2024-04-10", 90, 0, null, 3, 1, null, "WBS001" },
                    { 2, "BATCH002", 2, "Order 2", "2024-04-12", 140, 0, null, 3, 2, null, "WBS002" },
                    { 3, "BATCH003", 3, "Order 3", "2024-04-15", 180, 0, null, 3, 3, null, "WBS003" },
                    { 4, "BATCH004", 4, "Order 4", "2024-04-18", 110, 0, null, 3, 4, null, "WBS004" },
                    { 5, "BATCH005", 1, "Order 5", "2024-04-20", 80, 0, null, 3, 5, null, "WBS005" },
                    { 6, "BATCH006", 2, "Order 6", "2024-04-22", 170, 0, null, 3, 1, null, "WBS006" },
                    { 7, "BATCH007", 3, "Order 7", "2024-04-25", 200, 0, null, 3, 2, null, "WBS007" },
                    { 8, "BATCH008", 4, "Order 8", "2024-04-28", 120, 0, null, 3, 3, null, "WBS008" },
                    { 9, "BATCH009", 5, "Order 9", "2024-05-01", 90, 0, null, 3, 4, null, "WBS009" },
                    { 10, "BATCH010", 1, "Order 10", "2024-05-03", 140, 0, null, 3, 5, null, "WBS010" },
                    { 11, "BATCH011", 2, "Order 11", "2024-05-06", 180, 0, null, 3, 1, null, "WBS011" },
                    { 12, "BATCH012", 3, "Order 12", "2024-05-09", 100, 0, null, 3, 2, null, "WBS012" },
                    { 13, "BATCH013", 4, "Order 13", "2024-05-12", 150, 0, null, 3, 3, null, "WBS013" },
                    { 14, "BATCH014", 5, "Order 14", "2024-05-15", 180, 0, null, 3, 4, null, "WBS014" },
                    { 15, "BATCH015", 1, "Order 15", "2024-05-18", 70, 0, null, 3, 5, null, "WBS015" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "orderId",
                keyValue: 15);

            migrationBuilder.RenameColumn(
                name: "Wbs",
                table: "Orders",
                newName: "WBS");
        }
    }
}
