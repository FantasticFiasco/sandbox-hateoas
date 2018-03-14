using System.Collections.Generic;
using System.Linq;
using Bogus;
using Hateoas.Models;

namespace Hateoas.Services
{
    public class CommentService
    {
        private readonly IDictionary<int, Comment> comments;

        public CommentService(AuthorService authorService, ArticleService articleService)
        {
            comments = new Dictionary<int, Comment>(Seed(authorService, articleService));
        }

        public Comment Add(CommentToAdd commentToAdd)
        {
            var comment = new Comment(
                comments.GetNextId(),
                commentToAdd.AuthorId,
                commentToAdd.ArticleId,
                commentToAdd.Message);

            comments.Add(comment.Id, comment);

            return comment;
        }

        public Comment Get(int id)
        {
            comments.TryGetValue(id, out var comment);
            return comment;
        }

        public Comment[] GetAll(int articleId)
        {
            return comments.Values
                .Where(comment => comment.ArticleId == articleId)
                .ToArray();
        }

        public bool Remove(int id)
        {
            return comments.Remove(id);
        }

        private static IDictionary<int, Comment> Seed(AuthorService authorService, ArticleService articleService)
        {
            var authorIds = authorService.GetAll().Select(author => author.Id);
            var articleIds = articleService.GetAll().Select(article => article.Id);

            var commentFaker = new Faker<Comment>()
                .CustomInstantiator(faker =>
                    new Comment(
                        faker.IndexFaker + 1,
                        faker.PickRandom(authorIds),
                        faker.PickRandom(articleIds),
                        faker.Lorem.Sentence()));

            return commentFaker.Generate(5 * articleIds.Count())
                .ToDictionary(comment => comment.Id);
        }
    }
}
