using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class Trivisa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 3, 13, 3, 57, 354, DateTimeKind.Local).AddTicks(9607), new DateTime(2022, 2, 24, 13, 3, 57, 353, DateTimeKind.Local).AddTicks(7533) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 3, 12, 44, 31, 446, DateTimeKind.Local).AddTicks(1460), new DateTime(2022, 2, 24, 12, 44, 31, 445, DateTimeKind.Local).AddTicks(212) });
        }
    }
}
