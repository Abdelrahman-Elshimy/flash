using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class usertrivia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTriviaGifteds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TriviaGiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTriviaGifteds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTriviaGifteds_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTriviaGifteds_TriviaGift_TriviaGiftId",
                        column: x => x.TriviaGiftId,
                        principalTable: "TriviaGift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 3, 15, 11, 17, 194, DateTimeKind.Local).AddTicks(8269), new DateTime(2022, 2, 24, 15, 11, 17, 193, DateTimeKind.Local).AddTicks(4958) });

            migrationBuilder.CreateIndex(
                name: "IX_UserTriviaGifteds_TriviaGiftId",
                table: "UserTriviaGifteds",
                column: "TriviaGiftId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTriviaGifteds_UserId",
                table: "UserTriviaGifteds",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTriviaGifteds");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 3, 14, 2, 34, 247, DateTimeKind.Local).AddTicks(7830), new DateTime(2022, 2, 24, 14, 2, 34, 246, DateTimeKind.Local).AddTicks(892) });
        }
    }
}
