// https://stenbrinke.nl/blog/taking-ef-core-data-seeding-to-the-next-level-with-bogus/

using Bogus;
using Libry.Domain.Entities;

namespace Libry.Infrastructure;

public sealed class DatabaseSeeder
{
    public Library Library;
    public IReadOnlyCollection<Author> Authors = [];
    public IReadOnlyCollection<Book> Books = [];

    public DatabaseSeeder()
    {
        Authors = GenerateAuthors(100);
        Library = GenerateLibrary();
        Books = GenerateBooks(1000);
    }

    private static IReadOnlyCollection<Author> GenerateAuthors(int count)
    {
        var authors = new Faker<Author>()
            .RuleFor(x => x.Id, f => f.Database.Random.Guid())
            .RuleFor(x => x.GivenName, f => f.Person.FirstName)
            .RuleFor(x => x.FamilyName, f => f.Person.LastName)
            .RuleFor(x => x.BirthDate, f => f.Date.PastDateOnly(f.Random.Int(18, 399)))
            .RuleFor(x => x.AdditionalName, f => f.Person.FirstName)
            .Generate(count);

        return authors;
    }

    private List<Book> GenerateBooks(int count)
    {
        var books = new Faker<Book>()
            .RuleFor(x => x.Id, f => f.Database.Random.Guid())
            .RuleFor(x => x.DatePublished, f => f.Date.PastDateOnly())
            .RuleFor(x => x.Title, f => f.Commerce.ProductName())
            .RuleFor(x => x.NumberOfPages, f => f.Random.Int(123, 1302))
            .RuleFor(x => x.Authors, f => [.. f.PickRandom(Authors, 2)])
            .RuleFor(x => x.Library, f => Library)
            .Generate(count);

        return books;
    }

    private static Library GenerateLibrary()
    {
        var library = new Faker<Library>()
            .RuleFor(x => x.Id, f => f.Database.Random.Guid())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.Name, f => f.Company.CompanyName())
            .Generate();

        return library;
    }
}
