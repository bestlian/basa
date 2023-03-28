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
                    RoleName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: true),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: true),
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
                    WordID = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    Word = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Desc = table.Column<string>(type: "text", nullable: true),
                    Indonesian = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    English = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: true),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: true),
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
                    UserID = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RoleID = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: true),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: true),
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
                    ID = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    FirstWord = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    SecondWord = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: true),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: true),
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
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrErrorLogs",
                columns: table => new
                {
                    ErrorID = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    InnerException = table.Column<string>(type: "text", nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: false),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrErrorLogs", x => x.ErrorID);
                    table.ForeignKey(
                        name: "FK_TrErrorLogs_MsUsers_UserIn",
                        column: x => x.UserIn,
                        principalTable: "MsUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrUserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DeviceType = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedByIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ReplacedByToken = table.Column<string>(type: "text", nullable: true),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: true),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: true),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrUserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrUserRefreshTokens_MsUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "MsUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MsBasaLemes_FirstWord",
                table: "MsBasaLemes",
                column: "FirstWord");

            migrationBuilder.CreateIndex(
                name: "IX_MsUsers_RoleID",
                table: "MsUsers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TrErrorLogs_UserIn",
                table: "TrErrorLogs",
                column: "UserIn");

            migrationBuilder.CreateIndex(
                name: "IX_TrUserRefreshTokens_UserID",
                table: "TrUserRefreshTokens",
                column: "UserID");

            migrationBuilder.Sql("INSERT INTO public.\"MsRoles\" (\"RoleID\", \"RoleName\", \"DateIn\", \"IsDeleted\") VALUES(1, 'super admin', current_timestamp, false),(2, 'admin', current_timestamp, false),(3, 'staff', current_timestamp, false),(4, 'contributor', current_timestamp, false);");
            migrationBuilder.Sql("INSERT INTO public.\"MsUsers\" (\"UserID\", \"Email\", \"Password\", \"RoleID\", \"Name\", \"UserIn\", \"UserUp\", \"DateIn\", \"DateUp\", \"IsDeleted\") VALUES('c2b47e63-a625-43d3-b8ab-4e0e8e61d3d7'::uuid, 'admin@basa.id', '$2a$11$DMVT28XqNcCOJsw8RzCqcO7uNZlDzheuovX8n3/BMHTHpbAtXi56S', 1, 'Super Admin', NULL, NULL, '2023-03-27 14:41:21.657', NULL, false);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsBasaLemes");

            migrationBuilder.DropTable(
                name: "TrErrorLogs");

            migrationBuilder.DropTable(
                name: "TrUserRefreshTokens");

            migrationBuilder.DropTable(
                name: "MsWordLists");

            migrationBuilder.DropTable(
                name: "MsUsers");

            migrationBuilder.DropTable(
                name: "MsRoles");
        }
    }
}
