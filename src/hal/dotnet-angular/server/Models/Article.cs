namespace Hateoas.Models
{
    public class Article : ArticleToAddOrUpdate
    {
        public Article(int id, int authorId, string title, string body)
            : base(authorId, title, body)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
