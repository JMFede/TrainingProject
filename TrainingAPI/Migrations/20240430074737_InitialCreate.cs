using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    lineId = table.Column<int>(type: "int", nullable: false),
                    lineName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lines__32489DA535D4B69E", x => x.lineId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    statusId = table.Column<int>(type: "int", nullable: false),
                    statusName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderSta__36257A18CCD15231", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    typeId = table.Column<int>(type: "int", nullable: false),
                    typeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Types__F04DF13AE3DA98D7", x => x.typeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    userName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CFF58412903", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    typeId = table.Column<int>(type: "int", nullable: true),
                    lineId = table.Column<int>(type: "int", nullable: true),
                    batch = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    plannedQuantity = table.Column<int>(type: "int", nullable: true),
                    plannedDate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true),
                    WBS = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    startingDate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    statusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__0809335DECC5D2B3", x => x.orderId);
                    table.ForeignKey(
                        name: "FK__Orders__lineId__5070F446",
                        column: x => x.lineId,
                        principalTable: "Lines",
                        principalColumn: "lineId");
                    table.ForeignKey(
                        name: "FK__Orders__statusId__52593CB8",
                        column: x => x.statusId,
                        principalTable: "OrderStatus",
                        principalColumn: "statusId");
                    table.ForeignKey(
                        name: "FK__Orders__typeId__4F7CD00D",
                        column: x => x.typeId,
                        principalTable: "Types",
                        principalColumn: "typeId");
                    table.ForeignKey(
                        name: "FK__Orders__userId__5165187F",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_lineId",
                table: "Orders",
                column: "lineId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_statusId",
                table: "Orders",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_typeId",
                table: "Orders",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
