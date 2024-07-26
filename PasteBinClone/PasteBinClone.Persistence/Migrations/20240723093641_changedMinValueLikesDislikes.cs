using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasteBinClone.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changedMinValueLikesDislikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Likes",
                table: "Pastes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AlterColumn<long>(
                name: "Dislikes",
                table: "Pastes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");

            migrationBuilder.AddCheckConstraint(
                name: "MinDislikesLength",
                table: "Pastes",
                sql: "Dislikes >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "MinLikesLength",
                table: "Pastes",
                sql: "Likes >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "MinDislikesLength",
                table: "Pastes");

            migrationBuilder.DropCheckConstraint(
                name: "MinLikesLength",
                table: "Pastes");

            migrationBuilder.AlterColumn<decimal>(
                name: "Likes",
                table: "Pastes",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);

            migrationBuilder.AlterColumn<decimal>(
                name: "Dislikes",
                table: "Pastes",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldDefaultValue: 0L);
        }
    }
}
