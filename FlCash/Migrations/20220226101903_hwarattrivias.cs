using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class hwarattrivias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountOfQuestionsSolved",
                table: "HwaratTriviaMissionUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 19, 2, 127, DateTimeKind.Local).AddTicks(9251), new DateTime(2022, 2, 26, 12, 19, 2, 126, DateTimeKind.Local).AddTicks(2847) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfQuestionsSolved",
                table: "HwaratTriviaMissionUsers");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 16, 55, 874, DateTimeKind.Local).AddTicks(8537), new DateTime(2022, 2, 26, 12, 16, 55, 873, DateTimeKind.Local).AddTicks(7206) });
        }
    }
}
