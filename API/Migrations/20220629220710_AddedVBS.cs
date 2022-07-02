using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class AddedVBS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VBatterySystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCapacity = table.Column<double>(type: "REAL", nullable: false),
                    DepthOfDischarge = table.Column<double>(type: "REAL", nullable: false),
                    ContinuousDischargeRate = table.Column<double>(type: "REAL", nullable: false),
                    ContinuousChargeRate = table.Column<double>(type: "REAL", nullable: false),
                    ChargeEfficiency = table.Column<double>(type: "REAL", nullable: false),
                    DischargeEfficiency = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBatterySystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VBatterySystems_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VBatteryStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BatterySystemId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChargeLevel = table.Column<double>(type: "REAL", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VBatteryStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VBatteryStates_VBatterySystems_BatterySystemId",
                        column: x => x.BatterySystemId,
                        principalTable: "VBatterySystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VBatteryStates_BatterySystemId",
                table: "VBatteryStates",
                column: "BatterySystemId");

            migrationBuilder.CreateIndex(
                name: "IX_VBatterySystems_AppUserId",
                table: "VBatterySystems",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VBatteryStates");

            migrationBuilder.DropTable(
                name: "VBatterySystems");
        }
    }
}
