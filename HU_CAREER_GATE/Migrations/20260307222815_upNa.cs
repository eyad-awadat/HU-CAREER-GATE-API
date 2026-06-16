using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class upNa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmission_HRTask_HRTaskId",
                table: "TaskSubmission");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmission_HRTaskId",
                table: "TaskSubmission");

            migrationBuilder.DropColumn(
                name: "HRTaskId",
                table: "TaskSubmission");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_TaskId",
                table: "TaskSubmission",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmission_HRTask_TaskId",
                table: "TaskSubmission",
                column: "TaskId",
                principalTable: "HRTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmission_HRTask_TaskId",
                table: "TaskSubmission");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmission_TaskId",
                table: "TaskSubmission");

            migrationBuilder.AddColumn<int>(
                name: "HRTaskId",
                table: "TaskSubmission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_HRTaskId",
                table: "TaskSubmission",
                column: "HRTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmission_HRTask_HRTaskId",
                table: "TaskSubmission",
                column: "HRTaskId",
                principalTable: "HRTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
