using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTaskProject.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationsixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UsersTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UsersTasks");
        }
    }
}
