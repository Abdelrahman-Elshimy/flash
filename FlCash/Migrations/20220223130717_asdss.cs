using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class asdss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hearts",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 2, 15, 7, 13, 840, DateTimeKind.Local).AddTicks(84), new DateTime(2022, 2, 23, 15, 7, 13, 834, DateTimeKind.Local).AddTicks(2273) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hearts",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 2, 14, 33, 24, 467, DateTimeKind.Local).AddTicks(7378), new DateTime(2022, 2, 23, 14, 33, 24, 466, DateTimeKind.Local).AddTicks(269) });
        }
    }
}
