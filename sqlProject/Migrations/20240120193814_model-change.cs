using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sqlProject.Migrations
{
    /// <inheritdoc />
    public partial class modelchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AchievementLoggingAnswerAnswersID",
                table: "Answers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AchievementLoggingAnswerLoggingsID",
                table: "Answers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AchievementLoggingAnswerAnswersID",
                table: "AchievementLogging",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AchievementLoggingAnswerLoggingsID",
                table: "AchievementLogging",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "Answers",
                columns: new[] { "AchievementLoggingAnswerAnswersID", "AchievementLoggingAnswerLoggingsID" });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLogging_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "AchievementLogging",
                columns: new[] { "AchievementLoggingAnswerAnswersID", "AchievementLoggingAnswerLoggingsID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AchievementLogging_AchievementLoggingAnswer_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "AchievementLogging",
                columns: new[] { "AchievementLoggingAnswerAnswersID", "AchievementLoggingAnswerLoggingsID" },
                principalTable: "AchievementLoggingAnswer",
                principalColumns: new[] { "AnswersID", "LoggingsID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_AchievementLoggingAnswer_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "Answers",
                columns: new[] { "AchievementLoggingAnswerAnswersID", "AchievementLoggingAnswerLoggingsID" },
                principalTable: "AchievementLoggingAnswer",
                principalColumns: new[] { "AnswersID", "LoggingsID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AchievementLogging_AchievementLoggingAnswer_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "AchievementLogging");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_AchievementLoggingAnswer_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_AchievementLogging_AchievementLoggingAnswerAnswersID_AchievementLoggingAnswerLoggingsID",
                table: "AchievementLogging");

            migrationBuilder.DropColumn(
                name: "AchievementLoggingAnswerAnswersID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AchievementLoggingAnswerLoggingsID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AchievementLoggingAnswerAnswersID",
                table: "AchievementLogging");

            migrationBuilder.DropColumn(
                name: "AchievementLoggingAnswerLoggingsID",
                table: "AchievementLogging");
        }
    }
}
