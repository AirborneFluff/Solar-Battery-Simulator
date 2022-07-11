using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class removesdefualt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultSystemId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "VBatterySystems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "VBatterySystems");

            migrationBuilder.AddColumn<int>(
                name: "DefaultSystemId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
        }
    }
}
