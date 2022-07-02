using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class ExtendedVBS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "RealExportValue",
                table: "VBatterySystems",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RealImportValue",
                table: "VBatterySystems",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VirtualExportValue",
                table: "VBatterySystems",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VirtualImportValue",
                table: "VBatterySystems",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealExportValue",
                table: "VBatterySystems");

            migrationBuilder.DropColumn(
                name: "RealImportValue",
                table: "VBatterySystems");

            migrationBuilder.DropColumn(
                name: "VirtualExportValue",
                table: "VBatterySystems");

            migrationBuilder.DropColumn(
                name: "VirtualImportValue",
                table: "VBatterySystems");
        }
    }
}
