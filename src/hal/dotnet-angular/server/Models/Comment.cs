namespace Hateoas.Models
{
    public class Comment : CommentToAdd
    {
        public Comment(int id, int authorId, int articleId, string message)
            : base(authorId, articleId, message)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
