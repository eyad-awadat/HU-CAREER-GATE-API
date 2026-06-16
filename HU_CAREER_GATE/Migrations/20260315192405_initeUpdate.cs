using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class initeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeSubmission",
                table: "TaskSubmission",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeSubmission",
                table: "TaskSubmission");
        }
    }
}
