using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddTableFull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Tasks",
                newName: "TaskSubject");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Tasks",
                newName: "TaskDescription");

            migrationBuilder.AddColumn<string>(
                name: "TaskPdfName",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskSubmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    TimeTakenInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSubmission_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSubmission_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_StudentId",
                table: "TaskSubmission",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmission_TaskId",
                table: "TaskSubmission",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskSubmission");

            migrationBuilder.DropColumn(
                name: "TaskPdfName",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskSubject",
                table: "Tasks",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "TaskDescription",
                table: "Tasks",
                newName: "Description");
        }
    }
}
