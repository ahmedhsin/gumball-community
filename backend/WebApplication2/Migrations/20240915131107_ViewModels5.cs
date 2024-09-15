using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Migrations
{
    /// <inheritdoc />
    public partial class ViewModels5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Comments_CommentId",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_CommentId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Reactions");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Reactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Reactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "Reactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_CommentId",
                table: "Reactions",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Comments_CommentId",
                table: "Reactions",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id");
        }
    }
}
