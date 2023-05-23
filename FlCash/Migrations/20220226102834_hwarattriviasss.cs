using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class hwarattriviasss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Modes_ModeId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "Missions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_ModeId",
                table: "Missions",
                newName: "IX_Missions_CategoryId");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 28, 34, 26, DateTimeKind.Local).AddTicks(1446), new DateTime(2022, 2, 26, 12, 28, 34, 24, DateTimeKind.Local).AddTicks(9143) });

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Categories_CategoryId",
                table: "Missions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Categories_CategoryId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Missions",
                newName: "ModeId");

            migrationBuilder.RenameIndex(
                name: "IX_Missions_CategoryId",
                table: "Missions",
                newName: "IX_Missions_ModeId");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 24, 38, 161, DateTimeKind.Local).AddTicks(264), new DateTime(2022, 2, 26, 12, 24, 38, 159, DateTimeKind.Local).AddTicks(7924) });

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Modes_ModeId",
                table: "Missions",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
