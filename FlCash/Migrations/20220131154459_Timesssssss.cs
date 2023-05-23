using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class Timesssssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ElmarkaTriesUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 2, 7, 17, 44, 58, 522, DateTimeKind.Local).AddTicks(5061), new DateTime(2022, 1, 31, 17, 44, 58, 521, DateTimeKind.Local).AddTicks(2334) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "ElmarkaTriesUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 2, 7, 15, 7, 8, 59, DateTimeKind.Local).AddTicks(5089), new DateTime(2022, 1, 31, 15, 7, 8, 57, DateTimeKind.Local).AddTicks(1555) });
        }
    }
}
