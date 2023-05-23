using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class cx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ModeDetail",
                columns: new[] { "Id", "End", "ModeId", "Start" },
                values: new object[] { 1, new DateTime(2022, 2, 7, 17, 44, 58, 522, DateTimeKind.Local).AddTicks(5061), 2L, new DateTime(2022, 1, 31, 17, 44, 58, 521, DateTimeKind.Local).AddTicks(2334) });
        }
    }
}
