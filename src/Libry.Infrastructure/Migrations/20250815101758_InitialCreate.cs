using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libry.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Author",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                GivenName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FamilyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                AdditionalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Author", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Library",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Library", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Book",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                NumberOfPages = table.Column<int>(type: "int", nullable: false),
                DatePublished = table.Column<DateOnly>(type: "date", nullable: false),
                LibraryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Book", x => x.Id);
                table.ForeignKey(
                    name: "FK_Book_Library_LibraryId",
                    column: x => x.LibraryId,
                    principalTable: "Library",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AuthorBook",
            columns: table => new
            {
                AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                table.ForeignKey(
                    name: "FK_AuthorBook_Author_AuthorsId",
                    column: x => x.AuthorsId,
                    principalTable: "Author",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AuthorBook_Book_BooksId",
                    column: x => x.BooksId,
                    principalTable: "Book",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AuthorIds_Descending",
            table: "Author",
            column: "Id",
            unique: true,
            descending: new bool[0]);

        migrationBuilder.CreateIndex(
            name: "IX_AuthorNames_Descending",
            table: "Author",
            columns: new[] { "FamilyName", "GivenName" },
            descending: new bool[0]);

        migrationBuilder.CreateIndex(
            name: "IX_AuthorBook_BooksId",
            table: "AuthorBook",
            column: "BooksId");

        migrationBuilder.CreateIndex(
            name: "IX_Book_LibraryId",
            table: "Book",
            column: "LibraryId");

        migrationBuilder.CreateIndex(
            name: "IX_BookIds_Descending",
            table: "Book",
            column: "Id",
            unique: true,
            descending: new bool[0]);

        migrationBuilder.CreateIndex(
            name: "IX_BookTitles_Descending",
            table: "Book",
            column: "Title",
            descending: new bool[0]);

        migrationBuilder.CreateIndex(
            name: "IX_LibraryIds_Descending",
            table: "Library",
            column: "Id",
            unique: true,
            descending: new bool[0]);

        migrationBuilder.CreateIndex(
            name: "IX_LibraryNames_Descending",
            table: "Library",
            column: "Name",
            descending: new bool[0]);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AuthorBook");

        migrationBuilder.DropTable(
            name: "Author");

        migrationBuilder.DropTable(
            name: "Book");

        migrationBuilder.DropTable(
            name: "Library");
    }
}
