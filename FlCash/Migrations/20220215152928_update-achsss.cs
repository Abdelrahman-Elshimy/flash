using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class updateachsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CollectedAchievements",
                newName: "ManId");

            migrationBuilder.RenameColumn(
                name: "AchievementId",
                table: "CollectedAchievements",
                newName: "AchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManId",
                table: "CollectedAchievements",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AchID",
                table: "CollectedAchievements",
                newName: "AchievementId");
        }
    }
}
