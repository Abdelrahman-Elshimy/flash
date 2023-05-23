using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class usertrivias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "UserTriviaGifteds",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 3, 15, 17, 14, 1, DateTimeKind.Local).AddTicks(3792), new DateTime(2022, 2, 24, 15, 17, 13, 999, DateTimeKind.Local).AddTicks(8759) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserTriviaGifteds");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 3, 15, 11, 17, 194, DateTimeKind.Local).AddTicks(8269), new DateTime(2022, 2, 24, 15, 11, 17, 193, DateTimeKind.Local).AddTicks(4958) });
        }
    }
}
