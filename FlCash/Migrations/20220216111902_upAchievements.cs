using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class upAchievements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CollectedAchievements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CollectedAchievements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
