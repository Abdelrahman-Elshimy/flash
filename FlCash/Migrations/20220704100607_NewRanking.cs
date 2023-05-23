using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class NewRanking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsInMonth",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointsInWeek",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsInMonth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PointsInWeek",
                table: "AspNetUsers");

            
        }
    }
}
