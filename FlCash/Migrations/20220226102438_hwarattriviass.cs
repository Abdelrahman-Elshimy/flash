using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class hwarattriviass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Missions",
                newName: "ModeId");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 24, 38, 161, DateTimeKind.Local).AddTicks(264), new DateTime(2022, 2, 26, 12, 24, 38, 159, DateTimeKind.Local).AddTicks(7924) });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_ModeId",
                table: "Missions",
                column: "ModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Modes_ModeId",
                table: "Missions",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Modes_ModeId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_ModeId",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "ModeId",
                table: "Missions",
                newName: "CategoryId");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 19, 2, 127, DateTimeKind.Local).AddTicks(9251), new DateTime(2022, 2, 26, 12, 19, 2, 126, DateTimeKind.Local).AddTicks(2847) });
        }
    }
}
