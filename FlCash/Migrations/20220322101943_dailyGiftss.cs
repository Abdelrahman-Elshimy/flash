using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class dailyGiftss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "DailyGifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "DailyGifts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreServiceId",
                table: "DailyGifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 29, 12, 19, 42, 536, DateTimeKind.Local).AddTicks(8070), new DateTime(2022, 3, 22, 12, 19, 42, 532, DateTimeKind.Local).AddTicks(4051) });

            migrationBuilder.CreateIndex(
                name: "IX_DailyGifts_StoreServiceId",
                table: "DailyGifts",
                column: "StoreServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyGifts_StoreServices_StoreServiceId",
                table: "DailyGifts",
                column: "StoreServiceId",
                principalTable: "StoreServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyGifts_StoreServices_StoreServiceId",
                table: "DailyGifts");

            migrationBuilder.DropIndex(
                name: "IX_DailyGifts_StoreServiceId",
                table: "DailyGifts");

            migrationBuilder.DropColumn(
                name: "StoreServiceId",
                table: "DailyGifts");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "DailyGifts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "DailyGifts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 29, 12, 11, 5, 3, DateTimeKind.Local).AddTicks(8438), new DateTime(2022, 3, 22, 12, 11, 5, 2, DateTimeKind.Local).AddTicks(1010) });
        }
    }
}
