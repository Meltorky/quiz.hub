using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quiz.hub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editquizentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionsNumber",
                table: "Quizzes");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Quizzes");

            migrationBuilder.AddColumn<int>(
                name: "QuestionsNumber",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
