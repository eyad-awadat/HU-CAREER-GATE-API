using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class AddDefultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "HRs",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "Padding",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "HRs",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true,
                oldDefaultValue: "Padding");
        }
    }
}
