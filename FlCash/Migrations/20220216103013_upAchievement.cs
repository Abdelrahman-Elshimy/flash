using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class upAchievement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collected",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "percentage",
                table: "Achievements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Collected",
                table: "Achievements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "percentage",
                table: "Achievements",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
