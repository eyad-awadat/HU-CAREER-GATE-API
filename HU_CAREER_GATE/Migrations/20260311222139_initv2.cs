using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HUCAREERGATE.Migrations
{
    /// <inheritdoc />
    public partial class initv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskQuestions_HRTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "HRTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskQuestions_TaskId",
                table: "TaskQuestions",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskQuestions");
        }
    }
}
