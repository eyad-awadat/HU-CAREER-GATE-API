using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class AddDecisionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DecisionDate",
                table: "TaskSubmission",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DecisionDate",
                table: "TaskSubmission");
        }
    }
}
