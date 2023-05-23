using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class UpdateUserBadeges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddsCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuysFromStoreCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InvitationCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayedGamesCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RightCounterCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SendGiftsCounter",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UploadedQuestion",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddsCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BuysFromStoreCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InvitationCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlayedGamesCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RightCounterCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SendGiftsCounter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UploadedQuestion",
                table: "AspNetUsers");

          
        }
    }
}
