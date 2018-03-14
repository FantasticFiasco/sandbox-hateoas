namespace Hateoas.Models
{
    public class ArticleToAddOrUpdate
    {
        public ArticleToAddOrUpdate(int authorId, string title, string body)
        {
            AuthorId = authorId;
            Title = title;
            Body = body;
        }

        public int AuthorId { get; }

        public string Title { get; }

        public string Body { get; }
    }
}
