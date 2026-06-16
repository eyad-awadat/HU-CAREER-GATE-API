using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class FixUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HRTask",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HRTask_UserId",
                table: "HRTask",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRTask_AspNetUsers_UserId",
                table: "HRTask",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRTask_AspNetUsers_UserId",
                table: "HRTask");

            migrationBuilder.DropIndex(
                name: "IX_HRTask_UserId",
                table: "HRTask");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HRTask");
        }
    }
}
