using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBinClone.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addPasswordToPaste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Pastes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Pastes");
        }
    }
}
