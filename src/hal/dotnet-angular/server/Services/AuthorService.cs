using System.Collections.Generic;
using System.Linq;
using Bogus;
using Hateoas.Models;

namespace Hateoas.Services
{
    public class AuthorService
    {
        private readonly IDictionary<int, Author> authors;

        public AuthorService()
        {
            authors = new Dictionary<int, Author>(Seed());
        }

        public Author Add(AuthorToAddOrUpdate authorToAddOrUpdate)
        {
            var author = new Author(
                authors.GetNextId(),
                authorToAddOrUpdate.Name,
                authorToAddOrUpdate.Email);

            authors.Add(author.Id, author);

            return author;
        }

        public Author Get(int id)
        {
            authors.TryGetValue(id, out var author);
            return author;
        }

        public Author[] GetAll()
        {
            return authors.Values.ToArray();
        }

        public Author Update(int id, AuthorToAddOrUpdate authorToAddOrUpdate)
        {
            if (!authors.ContainsKey(id))
            {
                return null;
            }

            var updatedAuthor = new Author(
                id,
                authorToAddOrUpdate.Name,
                authorToAddOrUpdate.Email);

            authors[id] = updatedAuthor;

            return updatedAuthor;
        }

        public bool Remove(int id)
        {
            return authors.Remove(id);
        }

        private static IDictionary<int, Author> Seed()
        {
            var authorFaker = new Faker<Author>()
                .CustomInstantiator(faker =>
                    new Author(
                        faker.IndexFaker + 1,
                        faker.Name.FullName(),
                        faker.Internet.Email()));

            return authorFaker.Generate(3)
                .ToDictionary(author => author.Id);
        }
    }
}
