using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookRental.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InititalTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ISDN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOverdue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName" },
                values: new object[,]
                {
                    { 1, "Classics" },
                    { 2, "Dystopian" },
                    { 3, "Romance" },
                    { 4, "Fantasy" },
                    { 5, "Science Fiction" },
                    { 6, "Historical Fiction" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "UserEmail" },
                values: new object[,]
                {
                    { 1, "JOHN", "John@gmail.com" },
                    { 2, "Mike", "Janaki.kv2@gmail.com" },
                    { 3, "LASSE", "Lasse@gmail.com" },
                    { 4, "EMIL", "Email@gmail.com" },
                    { 5, "KARTSON", "Karston@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "GenreId", "ISDN", "IsAvailable", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", 1, "9780743273565", true, "The Great Gatsby" },
                    { 2, "Harper Lee", 1, "9780060935467", true, "To Kill a Mockingbird" },
                    { 3, "George Orwell", 2, "9780451524935", true, "1984" },
                    { 4, "Jane Austen", 3, "9780141199078", true, "Pride and Prejudice" },
                    { 5, "J.D. Salinger", 1, "9780316769488", true, "The Catcher in the Rye" },
                    { 6, "J.R.R. Tolkien", 4, "9780547928227", true, "The Hobbit" },
                    { 7, "Ray Bradbury", 5, "9781451673319", true, "Fahrenheit 451" },
                    { 8, "Markus Zusak", 6, "9780375842207", true, "The Book Thief" },
                    { 9, "Herman Melville", 1, "9781503280786", true, "Moby-Dick" },
                    { 10, "Leo Tolstoy", 6, "9781400079988", true, "War and Peace" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BookId",
                table: "Rentals",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
