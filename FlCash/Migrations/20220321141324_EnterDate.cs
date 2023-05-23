using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class EnterDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastEnter",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 28, 16, 13, 24, 10, DateTimeKind.Local).AddTicks(8313), new DateTime(2022, 3, 21, 16, 13, 24, 9, DateTimeKind.Local).AddTicks(5049) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastEnter",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 28, 34, 26, DateTimeKind.Local).AddTicks(1446), new DateTime(2022, 2, 26, 12, 28, 34, 24, DateTimeKind.Local).AddTicks(9143) });
        }
    }
}
