using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApi.Migrations;

/// <inheritdoc />
public partial class _20261126155816_Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Actor",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                BirthYear = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Actor", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Genre",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genre", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MovieDetails",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Synopsis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Budget = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovieDetails", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Movie",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Year = table.Column<int>(type: "int", nullable: false),
                Duration = table.Column<int>(type: "int", nullable: false),
                MovieDetailsId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Movie", x => x.Id);
                table.ForeignKey(
                    name: "FK_Movie_MovieDetails_MovieDetailsId",
                    column: x => x.MovieDetailsId,
                    principalTable: "MovieDetails",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MovieActor",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                MovieId = table.Column<int>(type: "int", nullable: false),
                ActorId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovieActor", x => x.Id);
                table.ForeignKey(
                    name: "FK_MovieActor_Actor_ActorId",
                    column: x => x.ActorId,
                    principalTable: "Actor",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_MovieActor_Movie_MovieId",
                    column: x => x.MovieId,
                    principalTable: "Movie",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "MovieGenre",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                GenreId = table.Column<int>(type: "int", nullable: false),
                MovieId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MovieGenre", x => x.Id);
                table.ForeignKey(
                    name: "FK_MovieGenre_Genre_GenreId",
                    column: x => x.GenreId,
                    principalTable: "Genre",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_MovieGenre_Movie_MovieId",
                    column: x => x.MovieId,
                    principalTable: "Movie",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Review",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ReviewerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false),
                MovieId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Review", x => x.Id);
                table.ForeignKey(
                    name: "FK_Review_Movie_MovieId",
                    column: x => x.MovieId,
                    principalTable: "Movie",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Actor",
            columns: new[] { "Id", "BirthYear", "Name" },
            values: new object[,]
            {
                { 1, 1973, "Seth MacFarlane" },
                { 2, 1971, "Mark Wahlberg" },
                { 3, 1983, "Mila Kunis" },
                { 4, 1971, "Joel McHale" },
                { 5, 1974, "Giovanni Ribisi" },
                { 6, 1964, "Patrick Warburton" },
                { 7, 1964, "Matt Walsh" },
                { 8, 1980, "Jessica Barth" },
                { 9, 1947, "Bill Smitrovich" },
                { 10, 1940, "Patrick Stewart" },
                { 11, 1985, "Amanda Seyfried" },
                { 12, 1937, "Morgan Freeman" },
                { 13, 1962, "John Slattery" },
                { 14, 1952, "Michael Dorn" }
            });

        migrationBuilder.InsertData(
            table: "Genre",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Action" },
                { 2, "Adventure" },
                { 3, "Comedy" },
                { 4, "Drama" },
                { 5, "Horror" },
                { 6, "Science Fiction" },
                { 7, "Fantasy" },
                { 8, "Romance" },
                { 9, "Thriller & Mystery" },
                { 10, "Documentary" }
            });

        migrationBuilder.InsertData(
            table: "MovieDetails",
            columns: new[] { "Id", "Budget", "Language", "Synopsis" },
            values: new object[,]
            {
                { 1, 60000000.0m, "English", "A man whose childhood wish of bringing his teddy bear to life came true must now decide whether that friendship is more important to him than his girlfriend." },
                { 2, 68000000.0m, "English", "Newlywed couple Ted and Tami-Lynn want to have a baby, but in order to qualify to be a parent, Ted will have to prove he's a person in a court of law." }
            });

        migrationBuilder.InsertData(
            table: "Movie",
            columns: new[] { "Id", "Duration", "MovieDetailsId", "Title", "Year" },
            values: new object[,]
            {
                { 1, 106, 1, "Ted", 2012 },
                { 2, 116, 2, "Ted 2", 2015 }
            });

        migrationBuilder.InsertData(
            table: "MovieActor",
            columns: new[] { "Id", "ActorId", "MovieId" },
            values: new object[,]
            {
                { 1, 1, 1 },
                { 2, 2, 1 },
                { 3, 3, 1 },
                { 4, 4, 1 },
                { 5, 5, 1 },
                { 6, 6, 1 },
                { 7, 7, 1 },
                { 8, 8, 1 },
                { 9, 9, 1 },
                { 10, 10, 1 },
                { 11, 1, 2 },
                { 12, 2, 2 },
                { 13, 11, 2 },
                { 14, 5, 2 },
                { 15, 8, 2 },
                { 16, 12, 2 },
                { 17, 13, 2 },
                { 18, 6, 2 },
                { 19, 14, 2 },
                { 20, 10, 2 }
            });

        migrationBuilder.InsertData(
            table: "MovieGenre",
            columns: new[] { "Id", "GenreId", "MovieId" },
            values: new object[,]
            {
                { 1, 3, 1 },
                { 2, 7, 1 },
                { 3, 3, 2 },
                { 4, 7, 2 }
            });

        migrationBuilder.InsertData(
            table: "Review",
            columns: new[] { "Id", "Comment", "MovieId", "Rating", "ReviewerName" },
            values: new object[,]
            {
                { 1, "En kanon film! Mycket skratt. Måste bara ses.", 1, 5, "Jopis" },
                { 2, "En fantastiskt bra uppföljare till Ted filmen. Lika rolig och spännande.", 2, 5, "Jopis" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Movie_MovieDetailsId",
            table: "Movie",
            column: "MovieDetailsId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MovieActor_ActorId",
            table: "MovieActor",
            column: "ActorId");

        migrationBuilder.CreateIndex(
            name: "IX_MovieActor_MovieId",
            table: "MovieActor",
            column: "MovieId");

        migrationBuilder.CreateIndex(
            name: "IX_MovieGenre_GenreId",
            table: "MovieGenre",
            column: "GenreId");

        migrationBuilder.CreateIndex(
            name: "IX_MovieGenre_MovieId",
            table: "MovieGenre",
            column: "MovieId");

        migrationBuilder.CreateIndex(
            name: "IX_Review_MovieId",
            table: "Review",
            column: "MovieId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MovieActor");

        migrationBuilder.DropTable(
            name: "MovieGenre");

        migrationBuilder.DropTable(
            name: "Review");

        migrationBuilder.DropTable(
            name: "Actor");

        migrationBuilder.DropTable(
            name: "Genre");

        migrationBuilder.DropTable(
            name: "Movie");

        migrationBuilder.DropTable(
            name: "MovieDetails");
    }
}
