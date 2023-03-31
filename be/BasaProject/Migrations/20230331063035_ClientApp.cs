using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasaProject.Migrations
{
    /// <inheritdoc />
    public partial class ClientApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrUserRefreshTokens_MsUsers_UserID",
                table: "TrUserRefreshTokens");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "TrUserRefreshTokens",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientID",
                table: "TrUserRefreshTokens",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrClientApps",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientSecret = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Hint = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UserIn = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUp = table.Column<Guid>(type: "uuid", nullable: false),
                    DateIn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrClientApps", x => x.ClientID);
                    table.ForeignKey(
                        name: "FK_TrClientApps_MsUsers_UserIn",
                        column: x => x.UserIn,
                        principalTable: "MsUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrUserRefreshTokens_ClientID",
                table: "TrUserRefreshTokens",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_TrClientApps_UserIn",
                table: "TrClientApps",
                column: "UserIn");

            migrationBuilder.AddForeignKey(
                name: "FK_TrUserRefreshTokens_MsUsers_UserID",
                table: "TrUserRefreshTokens",
                column: "UserID",
                principalTable: "MsUsers",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrUserRefreshTokens_TrClientApps_ClientID",
                table: "TrUserRefreshTokens",
                column: "ClientID",
                principalTable: "TrClientApps",
                principalColumn: "ClientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrUserRefreshTokens_MsUsers_UserID",
                table: "TrUserRefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TrUserRefreshTokens_TrClientApps_ClientID",
                table: "TrUserRefreshTokens");

            migrationBuilder.DropTable(
                name: "TrClientApps");

            migrationBuilder.DropIndex(
                name: "IX_TrUserRefreshTokens_ClientID",
                table: "TrUserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "TrUserRefreshTokens");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "TrUserRefreshTokens",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TrUserRefreshTokens_MsUsers_UserID",
                table: "TrUserRefreshTokens",
                column: "UserID",
                principalTable: "MsUsers",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
