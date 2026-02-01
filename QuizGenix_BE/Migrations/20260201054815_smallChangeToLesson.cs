using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGenix_BE.Migrations
{
    /// <inheritdoc />
    public partial class smallChangeToLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "lessons",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "lessons",
                newName: "status");
        }
    }
}
