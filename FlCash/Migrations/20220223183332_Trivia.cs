using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class Trivia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 2, 20, 33, 28, 331, DateTimeKind.Local).AddTicks(3024), new DateTime(2022, 2, 23, 20, 33, 28, 325, DateTimeKind.Local).AddTicks(480) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 2, 15, 7, 13, 840, DateTimeKind.Local).AddTicks(84), new DateTime(2022, 2, 23, 15, 7, 13, 834, DateTimeKind.Local).AddTicks(2273) });
        }
    }
}
