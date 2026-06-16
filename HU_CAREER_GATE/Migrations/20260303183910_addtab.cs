using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class addtab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmission_Tasks_TaskId",
                table: "TaskSubmission");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmission_TaskId",
                table: "TaskSubmission");

            migrationBuilder.AddColumn<int>(
                name: "HRTaskId",
                table: "TaskSubmission",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HRTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeInHours = table.Column<int>(type: "int", nullable: false),
                    TimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    TaskPdfName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HRTask_HRs_HrId",
                        column: x => x.HrId,
                        principalTable: "HRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_HRTaskId",
                table: "TaskSubmission",
                column: "HRTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_HRTask_HrId",
                table: "HRTask",
                column: "HrId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmission_HRTask_HRTaskId",
                table: "TaskSubmission",
                column: "HRTaskId",
                principalTable: "HRTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmission_HRTask_HRTaskId",
                table: "TaskSubmission");

            migrationBuilder.DropTable(
                name: "HRTask");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmission_HRTaskId",
                table: "TaskSubmission");

            migrationBuilder.DropColumn(
                name: "HRTaskId",
                table: "TaskSubmission");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HrId = table.Column<int>(type: "int", nullable: false),
                    JobLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskPdfName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeInHours = table.Column<int>(type: "int", nullable: false),
                    TimeInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_HRs_HrId",
                        column: x => x.HrId,
                        principalTable: "HRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_TaskId",
                table: "TaskSubmission",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_HrId",
                table: "Tasks",
                column: "HrId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmission_Tasks_TaskId",
                table: "TaskSubmission",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
