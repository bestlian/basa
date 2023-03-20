using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BasaProject.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsRoles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserIn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserUp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsRoles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "MsWordLists",
                columns: table => new
                {
                    WordID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Word = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Desc = table.Column<string>(type: "text", nullable: true),
                    Indonesian = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    English = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserIn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserUp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsWordLists", x => x.WordID);
                });

            migrationBuilder.CreateTable(
                name: "MsUsers",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RoleID = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", nullable: true),
                    UserIn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserUp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsUsers", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_MsUsers_MsRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "MsRoles",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "MsBasaLemes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstWord = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SecondWord = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserIn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserUp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsBasaLemes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MsBasaLemes_MsWordLists_FirstWord",
                        column: x => x.FirstWord,
                        principalTable: "MsWordLists",
                        principalColumn: "WordID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MsBasaLemes_FirstWord",
                table: "MsBasaLemes",
                column: "FirstWord");

            migrationBuilder.CreateIndex(
                name: "IX_MsUsers_RoleID",
                table: "MsUsers",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsBasaLemes");

            migrationBuilder.DropTable(
                name: "MsUsers");

            migrationBuilder.DropTable(
                name: "MsWordLists");

            migrationBuilder.DropTable(
                name: "MsRoles");
        }
    }
}
