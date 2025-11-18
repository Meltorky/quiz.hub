using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quiz.hub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editquizentity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuccessRate",
                table: "Quizzes",
                newName: "TotalScore");

            migrationBuilder.AddColumn<double>(
                name: "AverageScore",
                table: "Quizzes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageScore",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "Quizzes",
                newName: "SuccessRate");
        }
    }
}
