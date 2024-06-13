using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBinClone.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedExpireAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LifeTime",
                table: "Pastes",
                newName: "ExpireAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Pastes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Pastes");

            migrationBuilder.RenameColumn(
                name: "ExpireAt",
                table: "Pastes",
                newName: "LifeTime");
        }
    }
}
