using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sqlProject.Migrations
{
    /// <inheritdoc />
    public partial class TestModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOrderRandom",
                table: "Tests",
                newName: "IsQuestionsOrderRandom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsQuestionsOrderRandom",
                table: "Tests",
                newName: "IsOrderRandom");
        }
    }
}
