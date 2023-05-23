using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class newUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 2, 21, 16, 0, 14, 470, DateTimeKind.Local).AddTicks(7351), new DateTime(2022, 2, 14, 16, 0, 14, 469, DateTimeKind.Local).AddTicks(2821) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 2, 15, 15, 37, 7, 484, DateTimeKind.Local).AddTicks(6717), new DateTime(2022, 2, 8, 15, 37, 7, 482, DateTimeKind.Local).AddTicks(3833) });
        }
    }
}
