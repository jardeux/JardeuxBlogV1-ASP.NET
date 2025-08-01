using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JardeuxBlogV1.Migrations
{
    /// <inheritdoc />
    public partial class editmessagetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mesagge",
                table: "Comments",
                newName: "Message");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Comments",
                newName: "Mesagge");
        }
    }
}
