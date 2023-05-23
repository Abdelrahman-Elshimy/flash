using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class updateUserRankings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "AspNetUsers");

         
        }
    }
}
