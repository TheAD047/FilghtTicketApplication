using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilghtTicketApplication.Data.Migrations
{
    public partial class Flight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBooked",
                table: "Seat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBooked",
                table: "Seat");
        }
    }
}
