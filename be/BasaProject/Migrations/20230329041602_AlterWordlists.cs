using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasaProject.Migrations
{
    /// <inheritdoc />
    public partial class AlterWordlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "MsWordLists",
                type: "character varying(25)",
                maxLength: 25,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MsWordLists");
        }
    }
}
