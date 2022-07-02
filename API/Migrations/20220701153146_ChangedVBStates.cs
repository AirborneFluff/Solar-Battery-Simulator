using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class ChangedVBStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChargeLevel",
                table: "VBatterySystems",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "LoggingPeriod",
                table: "VBatterySystems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "RealExportValue",
                table: "VBatteryStates",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RealImportValue",
                table: "VBatteryStates",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VirtualExportValue",
                table: "VBatteryStates",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VirtualImportValue",
                table: "VBatteryStates",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeLevel",
                table: "VBatterySystems");

            migrationBuilder.DropColumn(
                name: "LoggingPeriod",
                table: "VBatterySystems");

            migrationBuilder.DropColumn(
                name: "RealExportValue",
                table: "VBatteryStates");

            migrationBuilder.DropColumn(
                name: "RealImportValue",
                table: "VBatteryStates");

            migrationBuilder.DropColumn(
                name: "VirtualExportValue",
                table: "VBatteryStates");

            migrationBuilder.DropColumn(
                name: "VirtualImportValue",
                table: "VBatteryStates");
        }
    }
}
