using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBinClone.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addDeletingConfigurationsToApiUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ApiUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_ApiUsers_UserId",
                table: "Ratings");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ApiUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "ApiUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_ApiUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "ApiUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ApiUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_ApiUsers_UserId",
                table: "Ratings");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ApiUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "ApiUsers",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_ApiUsers_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "ApiUsers",
                principalColumn: "UserId");
        }
    }
}
