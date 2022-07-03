using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addedsolarparams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SolarAz",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SolarDec",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SolarKwp",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SolarLat",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SolarLon",
                table: "AspNetUsers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolarAz",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SolarDec",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SolarKwp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SolarLat",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SolarLon",
                table: "AspNetUsers");
        }
    }
}
