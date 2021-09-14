using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoGamesApi.Api.Home.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfIssue = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfFoundation = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryEntityVideoGameEntity",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "int", nullable: false),
                    VideoGamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryEntityVideoGameEntity", x => new { x.CountriesId, x.VideoGamesId });
                    table.ForeignKey(
                        name: "FK_CountryEntityVideoGameEntity_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryEntityVideoGameEntity_Games_VideoGamesId",
                        column: x => x.VideoGamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreEntityVideoGameEntity",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    VideoGamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreEntityVideoGameEntity", x => new { x.GenresId, x.VideoGamesId });
                    table.ForeignKey(
                        name: "FK_GenreEntityVideoGameEntity_Games_VideoGamesId",
                        column: x => x.VideoGamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreEntityVideoGameEntity_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEntityVideoGameEntity",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    VideoGamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEntityVideoGameEntity", x => new { x.CompaniesId, x.VideoGamesId });
                    table.ForeignKey(
                        name: "FK_CompanyEntityVideoGameEntity_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyEntityVideoGameEntity_Games_VideoGamesId",
                        column: x => x.VideoGamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEntityVideoGameEntity_VideoGamesId",
                table: "CompanyEntityVideoGameEntity",
                column: "VideoGamesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryEntityVideoGameEntity_VideoGamesId",
                table: "CountryEntityVideoGameEntity",
                column: "VideoGamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreEntityVideoGameEntity_VideoGamesId",
                table: "GenreEntityVideoGameEntity",
                column: "VideoGamesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyEntityVideoGameEntity");

            migrationBuilder.DropTable(
                name: "CountryEntityVideoGameEntity");

            migrationBuilder.DropTable(
                name: "GenreEntityVideoGameEntity");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
