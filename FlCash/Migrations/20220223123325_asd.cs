using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class asd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ModeDetail",
                columns: new[] { "Id", "End", "ModeId", "Start" },
                values: new object[] { 1, new DateTime(2022, 3, 2, 14, 33, 24, 467, DateTimeKind.Local).AddTicks(7378), 2L, new DateTime(2022, 2, 23, 14, 33, 24, 466, DateTimeKind.Local).AddTicks(269) });
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
