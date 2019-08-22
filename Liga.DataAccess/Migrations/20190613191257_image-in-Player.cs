using Microsoft.EntityFrameworkCore.Migrations;

namespace Liga.DataAccess.Migrations
{
    public partial class imageinPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Players",
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Image",
                table: "Players",
                column: "Image",
                unique: true,
                filter: "[Image] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_Image",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Players",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 90);
        }
    }
}
