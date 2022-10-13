using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilghtTicketApplication.Data.Migrations
{
    public partial class FlightTicketApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "airlineName",
                table: "Flight");

            migrationBuilder.AddColumn<int>(
                name: "airlineID",
                table: "Flight",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    airlineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airlineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    airlineEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.airlineID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flight_airlineID",
                table: "Flight",
                column: "airlineID");

            migrationBuilder.AddForeignKey(
                name: "FK_Flight_Airline_airlineID",
                table: "Flight",
                column: "airlineID",
                principalTable: "Airline",
                principalColumn: "airlineID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flight_Airline_airlineID",
                table: "Flight");

            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropIndex(
                name: "IX_Flight_airlineID",
                table: "Flight");

            migrationBuilder.DropColumn(
                name: "airlineID",
                table: "Flight");

            migrationBuilder.AddColumn<string>(
                name: "airlineName",
                table: "Flight",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
