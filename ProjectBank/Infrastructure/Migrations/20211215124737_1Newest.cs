using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectBank.Infrastructure.Migrations
{
    public partial class _1Newest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AzureAdToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    Degree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Ects = table.Column<float>(type: "real", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Accounts_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountProject",
                columns: table => new
                {
                    AccountsId = table.Column<int>(type: "int", nullable: false),
                    SavedProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProject", x => new { x.AccountsId, x.SavedProjectsId });
                    table.ForeignKey(
                        name: "FK_AccountProject_Accounts_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountProject_Projects_SavedProjectsId",
                        column: x => x.SavedProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordProject",
                columns: table => new
                {
                    KeywordsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordProject", x => new { x.KeywordsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_KeywordProject_Keywords_KeywordsId",
                        column: x => x.KeywordsId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordProject_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountProject_SavedProjectsId",
                table: "AccountProject",
                column: "SavedProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AzureAdToken",
                table: "Accounts",
                column: "AzureAdToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeywordProject_ProjectsId",
                table: "KeywordProject",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_Word",
                table: "Keywords",
                column: "Word",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AuthorId",
                table: "Projects",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountProject");

            migrationBuilder.DropTable(
                name: "KeywordProject");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
