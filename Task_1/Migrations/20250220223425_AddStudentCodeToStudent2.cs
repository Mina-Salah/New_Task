using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_1.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentCodeToStudent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentCode",
                table: "Students",
                newName: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Students",
                newName: "StudentCode");
        }
    }
}
