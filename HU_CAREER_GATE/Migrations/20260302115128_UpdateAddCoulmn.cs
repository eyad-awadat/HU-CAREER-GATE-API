using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddCoulmn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvName",
                table: "HRs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvName",
                table: "HRs");
        }
    }
}
