using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilghtTicketApplication.Data.Migrations
{
    public partial class FlightTicketApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "seatRow",
                table: "Seat",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "flightName",
                table: "Flight",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flightName",
                table: "Flight");

            migrationBuilder.AlterColumn<string>(
                name: "seatRow",
                table: "Seat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
