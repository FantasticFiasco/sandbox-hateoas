namespace Hateoas.Models
{
    public class Author : AuthorToAddOrUpdate
    {
        public Author(int id, string name, string email)
            : base(name, email)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
