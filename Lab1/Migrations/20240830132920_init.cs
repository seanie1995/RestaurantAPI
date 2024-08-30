using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lab1.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartySize = table.Column<int>(type: "int", nullable: false),
                    BookingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_TableId = table.Column<int>(type: "int", nullable: true),
                    FK_CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Customer_FK_CustomerId",
                        column: x => x.FK_CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Table_FK_TableId",
                        column: x => x.FK_TableId,
                        principalTable: "Table",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Dish",
                columns: new[] { "Id", "Availability", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, "Cottage Cheese Pierogi", 89 },
                    { 2, true, "Russian Pierogi", 89 },
                    { 3, true, "Schabowy Pork Cutlet", 119 },
                    { 4, true, "Sour Rye Soup", 99 },
                    { 5, true, "Bigos", 99 }
                });

            migrationBuilder.InsertData(
                table: "Table",
                columns: new[] { "Id", "Capacity" },
                values: new object[,]
                {
                    { 1, 4 },
                    { 2, 4 },
                    { 3, 2 },
                    { 4, 6 },
                    { 5, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_FK_CustomerId",
                table: "Booking",
                column: "FK_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_FK_TableId",
                table: "Booking",
                column: "FK_TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Dish");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Table");
        }
    }
}
