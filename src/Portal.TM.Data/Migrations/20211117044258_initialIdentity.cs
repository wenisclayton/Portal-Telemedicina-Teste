using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portal.TM.Data.Migrations
{
    public partial class initialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(250)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyRoleClaims_MyRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MyRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "varchar(100)", nullable: true),
                    ClaimValue = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyUserClaims_MyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "MyUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "varchar(100)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_MyUserLogins_MyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "MyUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_MyUserRoles_MyRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "MyRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MyUserRoles_MyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "MyUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MyUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_MyUserTokens_MyUser_UserId",
                        column: x => x.UserId,
                        principalTable: "MyUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyRoleClaims_RoleId",
                table: "MyRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "MyRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "MyUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "MyUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MyUserClaims_UserId",
                table: "MyUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyUserLogins_UserId",
                table: "MyUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyUserRoles_RoleId",
                table: "MyUserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyRoleClaims");

            migrationBuilder.DropTable(
                name: "MyUserClaims");

            migrationBuilder.DropTable(
                name: "MyUserLogins");

            migrationBuilder.DropTable(
                name: "MyUserRoles");

            migrationBuilder.DropTable(
                name: "MyUserTokens");

            migrationBuilder.DropTable(
                name: "MyRoles");

            migrationBuilder.DropTable(
                name: "MyUser");
        }
    }
}
