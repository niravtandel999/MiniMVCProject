﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCTaskProject.Migrations
{
    /// <inheritdoc />
    public partial class profilePicture2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Profiles");
        }
    }
}
