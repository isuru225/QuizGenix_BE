using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizGenix_BE.Migrations
{
    /// <inheritdoc />
    public partial class changeCreateQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Fix invalid data first
            migrationBuilder.Sql(
                @"UPDATE ""Questions""
          SET ""CorrectAnswer"" = '0'
          WHERE ""CorrectAnswer"" IS NULL
             OR ""CorrectAnswer"" !~ '^\d+$';"
            );

            // 2. Convert column type safely
            migrationBuilder.Sql(
                @"ALTER TABLE ""Questions""
          ALTER COLUMN ""CorrectAnswer""
          TYPE integer
          USING ""CorrectAnswer""::integer;"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
