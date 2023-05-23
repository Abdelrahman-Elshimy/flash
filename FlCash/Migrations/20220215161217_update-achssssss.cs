using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class updateachssssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManId",
                table: "CollectedAchievements");

            migrationBuilder.RenameColumn(
                name: "AchID",
                table: "CollectedAchievements",
                newName: "AchievementId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CollectedAchievements",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectedAchievements_AchievementId",
                table: "CollectedAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectedAchievements_UserId",
                table: "CollectedAchievements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CollectedAchievements_Achievements_AchievementId",
                table: "CollectedAchievements",
                column: "AchievementId",
                principalTable: "Achievements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectedAchievements_AspNetUsers_UserId",
                table: "CollectedAchievements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollectedAchievements_Achievements_AchievementId",
                table: "CollectedAchievements");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectedAchievements_AspNetUsers_UserId",
                table: "CollectedAchievements");

            migrationBuilder.DropIndex(
                name: "IX_CollectedAchievements_AchievementId",
                table: "CollectedAchievements");

            migrationBuilder.DropIndex(
                name: "IX_CollectedAchievements_UserId",
                table: "CollectedAchievements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CollectedAchievements");

            migrationBuilder.RenameColumn(
                name: "AchievementId",
                table: "CollectedAchievements",
                newName: "AchID");

            migrationBuilder.AddColumn<string>(
                name: "ManId",
                table: "CollectedAchievements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
