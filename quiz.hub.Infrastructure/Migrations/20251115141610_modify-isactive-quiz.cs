using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quiz.hub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifyisactivequiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Quizzes");
        }
    }
}
