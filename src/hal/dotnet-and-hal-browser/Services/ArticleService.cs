using System.Collections.Generic;
using System.Linq;
using Bogus;
using Hateoas.Models;

namespace Hateoas.Services
{
    public class ArticleService
    {
        private readonly IDictionary<int, Article> articles;

        public ArticleService(AuthorService authorService)
        {
            articles = new Dictionary<int, Article>(Seed(authorService));
        }

        public Article Add(ArticleToAddOrUpdate articleToAddOrUpdate)
        {
            var article = new Article(
                articles.GetNextId(),
                articleToAddOrUpdate.AuthorId,
                articleToAddOrUpdate.Title,
                articleToAddOrUpdate.Body);

            articles.Add(article.Id, article);

            return article;
        }

        public Article Get(int id)
        {
            articles.TryGetValue(id, out var article);
            return article;
        }

        public Article[] GetByAuthor(int authorId)
        {
            return articles.Values
                .Where(article => article.AuthorId == authorId)
                .ToArray();
        }

        public Article[] GetAll()
        {
            return articles.Values.ToArray();
        }

        public Article Update(int id, ArticleToAddOrUpdate articleToAddOrUpdate)
        {
            if (!articles.ContainsKey(id))
            {
                return null;
            }

            var updatedArticle = new Article(
                id,
                articleToAddOrUpdate.AuthorId,
                articleToAddOrUpdate.Title,
                articleToAddOrUpdate.Body);

            articles[id] = updatedArticle;

            return updatedArticle;
        }

        public bool Remove(int id)
        {
            return articles.Remove(id);
        }

        private static IDictionary<int, Article> Seed(AuthorService authorService)
        {
            var authorIds = authorService.GetAll().Select(author => author.Id);

            var articleFaker = new Faker<Article>()
                .CustomInstantiator(faker =>
                    new Article(
                        faker.IndexFaker + 1,
                        faker.PickRandom(authorIds),
                        faker.Lorem.Sentence().TrimEnd('.'),
                        faker.Lorem.Paragraphs()));

            return articleFaker.Generate(5 * authorIds.Count())
                .ToDictionary(article => article.Id);
        }
    }
}
