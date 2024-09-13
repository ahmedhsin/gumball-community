using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Migrations
{
    /// <inheritdoc />
    public partial class FinalModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorFollow_Authors_FollowerId",
                table: "AuthorFollow");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorFollow_Authors_FollowerId",
                table: "AuthorFollow",
                column: "FollowerId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorFollow_Authors_FollowerId",
                table: "AuthorFollow");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorFollow_Authors_FollowerId",
                table: "AuthorFollow",
                column: "FollowerId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
