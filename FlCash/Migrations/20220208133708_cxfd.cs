using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class cxfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ModeDetail",
                columns: new[] { "Id", "End", "ModeId", "Start" },
                values: new object[] { 1, new DateTime(2022, 2, 15, 15, 37, 7, 484, DateTimeKind.Local).AddTicks(6717), 2L, new DateTime(2022, 2, 8, 15, 37, 7, 482, DateTimeKind.Local).AddTicks(3833) });
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
