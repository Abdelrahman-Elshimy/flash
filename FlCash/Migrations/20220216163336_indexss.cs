using Microsoft.EntityFrameworkCore.Migrations;

namespace FlCash.Migrations
{
    public partial class indexss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_Name",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Name_Rate",
                table: "Questions",
                columns: new[] { "Name", "Rate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Questions_Name_Rate",
                table: "Questions");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Name",
                table: "Questions",
                column: "Name");
        }
    }
}
