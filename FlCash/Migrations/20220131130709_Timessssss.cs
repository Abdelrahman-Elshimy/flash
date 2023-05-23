using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class Timessssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ModeDetail",
                columns: new[] { "Id", "End", "ModeId", "Start" },
                values: new object[] { 1, new DateTime(2022, 2, 7, 15, 7, 8, 59, DateTimeKind.Local).AddTicks(5089), 2L, new DateTime(2022, 1, 31, 15, 7, 8, 57, DateTimeKind.Local).AddTicks(1555) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
