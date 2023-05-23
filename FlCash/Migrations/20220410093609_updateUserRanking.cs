using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class updateUserRanking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EasyLevelQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntertainmanetQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GeoQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HardLevelQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HistoryQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediumLevelQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScienceQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SportsQuestionCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EasyLevelQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EntertainmanetQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GeoQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HardLevelQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HistoryQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MediumLevelQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ScienceQuestionCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SportsQuestionCount",
                table: "AspNetUsers");

           
        }
    }
}
