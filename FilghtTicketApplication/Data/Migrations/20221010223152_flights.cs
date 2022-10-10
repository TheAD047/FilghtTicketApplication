using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilghtTicketApplication.Data.Migrations
{
    public partial class flights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    flightID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airlineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    landingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    departureFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arrivalAt = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.flightID);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ticketID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flightID = table.Column<int>(type: "int", nullable: false),
                    seatID = table.Column<int>(type: "int", nullable: false),
                    purchasedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.ticketID);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    seatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flightID = table.Column<int>(type: "int", nullable: false),
                    seatNum = table.Column<int>(type: "int", nullable: false),
                    seatRow = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.seatID);
                    table.ForeignKey(
                        name: "FK_Seat_Flight_flightID",
                        column: x => x.flightID,
                        principalTable: "Flight",
                        principalColumn: "flightID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seat_flightID",
                table: "Seat",
                column: "flightID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Flight");
        }
    }
}
