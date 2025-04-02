using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using MyProject.Application.DTOs;

namespace MyProject.Application.Extensions
{
    public static class DataSeeder
    {
        public static List<AuthorDto> GenerateAuthors(int count)
        {
            var authorFaker = new Faker<AuthorDto>()
                .RuleFor(a => a.Id, f => Guid.NewGuid())
                .RuleFor(a => a.Name, f => f.Name.FullName());

            return authorFaker.Generate(count);
        }

        public static List<GenreDto> GenerateGenres(int count)
        {
            var genreFaker = new Faker<GenreDto>()
                .RuleFor(g => g.Id, f => Guid.NewGuid())
                .RuleFor(g => g.Name, f => f.Commerce.Categories(1)[0]);

            return genreFaker.Generate(count);
        }

        public static List<BookDto> GenerateBooks(int count, List<AuthorDto> authors, List<GenreDto> genres)
        {
            var bookFaker = new Faker<BookDto>()
                .RuleFor(b => b.Id, f => Guid.NewGuid())
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
                .RuleFor(b => b.PublishYear, f => f.Date.Past(20))
                .RuleFor(b => b.Description, f => f.Lorem.Paragraph())
                .RuleFor(b => b.Page, f => f.Random.Int(100, 1000))
                .RuleFor(b => b.AuthorId, f => f.PickRandom(authors).Id)
                .RuleFor(b => b.GenreId, f => f.PickRandom(genres).Id);

            return bookFaker.Generate(count);
        }
    }
}
