using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sqlProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoggingTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggingTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsOrderRandom = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    IsUsing = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerTestID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_OwnerTestID",
                        column: x => x.OwnerTestID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    TypeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_TypeID",
                        column: x => x.TypeID,
                        principalTable: "UserTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerQuestionID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_OwnerQuestionID",
                        column: x => x.OwnerQuestionID,
                        principalTable: "Questions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementLogging",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentID = table.Column<int>(type: "INTEGER", nullable: false),
                    TestID = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLogging", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AchievementLogging_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLogging_Users_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logging",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Time = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    LoggingTypeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logging", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logging_LoggingTypes_LoggingTypeID",
                        column: x => x.LoggingTypeID,
                        principalTable: "LoggingTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logging_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchievementLoggingAnswer",
                columns: table => new
                {
                    AnswersID = table.Column<int>(type: "INTEGER", nullable: false),
                    LoggingsID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementLoggingAnswer", x => new { x.AnswersID, x.LoggingsID });
                    table.ForeignKey(
                        name: "FK_AchievementLoggingAnswer_AchievementLogging_LoggingsID",
                        column: x => x.LoggingsID,
                        principalTable: "AchievementLogging",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievementLoggingAnswer_Answers_AnswersID",
                        column: x => x.AnswersID,
                        principalTable: "Answers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LoggingTypes",
                columns: new[] { "ID", "TypeName" },
                values: new object[,]
                {
                    { 1, "Вход" },
                    { 2, "Выход" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "ID", "TypeName" },
                values: new object[,]
                {
                    { 1, "Админ" },
                    { 2, "Студент" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLogging_StudentID",
                table: "AchievementLogging",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLogging_TestID",
                table: "AchievementLogging",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementLoggingAnswer_LoggingsID",
                table: "AchievementLoggingAnswer",
                column: "LoggingsID");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_OwnerQuestionID",
                table: "Answers",
                column: "OwnerQuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Logging_LoggingTypeID",
                table: "Logging",
                column: "LoggingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Logging_UserID",
                table: "Logging",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_OwnerTestID",
                table: "Questions",
                column: "OwnerTestID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TypeID",
                table: "Users",
                column: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementLoggingAnswer");

            migrationBuilder.DropTable(
                name: "Logging");

            migrationBuilder.DropTable(
                name: "AchievementLogging");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "LoggingTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
