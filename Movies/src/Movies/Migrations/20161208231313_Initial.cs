using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    Name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Released = table.Column<string>(nullable: false),
                    Awards = table.Column<string>(maxLength: 128, nullable: false),
                    Director = table.Column<string>(maxLength: 64, nullable: false),
                    ImdbRating = table.Column<string>(maxLength: 4, nullable: false),
                    ImdbVotes = table.Column<string>(maxLength: 128, nullable: false),
                    Metascore = table.Column<string>(maxLength: 4, nullable: false),
                    Plot = table.Column<string>(maxLength: 512, nullable: false),
                    Runtime = table.Column<string>(maxLength: 32, nullable: false),
                    TomatoMeter = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => new { x.Title, x.Released });
                });

            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    Title = table.Column<string>(nullable: false),
                    Released = table.Column<string>(nullable: false),
                    GenreId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => new { x.Title, x.Released, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenre_Movies_Title_Released",
                        columns: x => new { x.Title, x.Released },
                        principalTable: "Movies",
                        principalColumns: new[] { "Title", "Released" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ImdbRating",
                table: "Movies",
                column: "ImdbRating");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ImdbVotes",
                table: "Movies",
                column: "ImdbVotes");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Metascore",
                table: "Movies",
                column: "Metascore");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_TomatoMeter",
                table: "Movies",
                column: "TomatoMeter");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_Title_Released",
                table: "MovieGenre",
                columns: new[] { "Title", "Released" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenre");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
