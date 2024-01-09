using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sqlProject.Migrations
{
    /// <inheritdoc />
    public partial class INotifyPropertyChangeTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "TypeName",
                value: "Учитель");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "ID",
                keyValue: 1,
                column: "TypeName",
                value: "Админ");
        }
    }
}
