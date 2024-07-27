using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBinClone.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNullableToTypeAndLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_ContentTypes_TypeId",
                table: "Pastes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_Languages_LanguageId",
                table: "Pastes");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Pastes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Pastes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_ContentTypes_TypeId",
                table: "Pastes",
                column: "TypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_Languages_LanguageId",
                table: "Pastes",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_ContentTypes_TypeId",
                table: "Pastes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pastes_Languages_LanguageId",
                table: "Pastes");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Pastes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "Pastes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_ContentTypes_TypeId",
                table: "Pastes",
                column: "TypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pastes_Languages_LanguageId",
                table: "Pastes",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
