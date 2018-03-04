namespace Hateoas.Models
{
    public class CommentToAdd
    {
        public CommentToAdd(int authorId, int articleId, string message)
        {
            AuthorId = authorId;
            ArticleId = articleId;
            Message = message;
        }

        public int AuthorId { get; }

        public int ArticleId { get; }

        public string Message { get; }
    }
}
