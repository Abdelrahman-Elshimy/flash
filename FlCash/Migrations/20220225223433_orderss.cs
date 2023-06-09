﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class orderss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 0, 34, 32, 209, DateTimeKind.Local).AddTicks(3277), new DateTime(2022, 2, 26, 0, 34, 32, 207, DateTimeKind.Local).AddTicks(5307) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ModeDetail",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "End", "Start" },
                values: new object[] { new DateTime(2022, 3, 5, 0, 4, 49, 194, DateTimeKind.Local).AddTicks(4799), new DateTime(2022, 2, 26, 0, 4, 49, 192, DateTimeKind.Local).AddTicks(7927) });
        }
    }
}
