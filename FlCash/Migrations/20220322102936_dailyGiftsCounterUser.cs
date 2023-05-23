using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class dailyGiftsCounterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterDailyGift",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 29, 12, 29, 35, 636, DateTimeKind.Local).AddTicks(9423), new DateTime(2022, 3, 22, 12, 29, 35, 634, DateTimeKind.Local).AddTicks(6594) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterDailyGift",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 29, 12, 19, 42, 536, DateTimeKind.Local).AddTicks(8070), new DateTime(2022, 3, 22, 12, 19, 42, 532, DateTimeKind.Local).AddTicks(4051) });
        }
    }
}
