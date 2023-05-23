using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class hwarattrivia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hwaratTrivias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionsCompleted = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hwaratTrivias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hwaratTrivias_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionCount = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HwaratTriviaMissionUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HwaratTriviaMissionUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HwaratTriviaMissionUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HwaratTriviaMissionUsers_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 12, 16, 55, 874, DateTimeKind.Local).AddTicks(8537), new DateTime(2022, 2, 26, 12, 16, 55, 873, DateTimeKind.Local).AddTicks(7206) });

            migrationBuilder.CreateIndex(
                name: "IX_HwaratTriviaMissionUsers_MissionId",
                table: "HwaratTriviaMissionUsers",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_HwaratTriviaMissionUsers_UserId",
                table: "HwaratTriviaMissionUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_hwaratTrivias_UserId",
                table: "hwaratTrivias",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HwaratTriviaMissionUsers");

            migrationBuilder.DropTable(
                name: "hwaratTrivias");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 0, 34, 32, 209, DateTimeKind.Local).AddTicks(3277), new DateTime(2022, 2, 26, 0, 34, 32, 207, DateTimeKind.Local).AddTicks(5307) });
        }
    }
}
