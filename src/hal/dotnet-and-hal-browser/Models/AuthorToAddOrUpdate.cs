namespace Hateoas.Models
{
    public class AuthorToAddOrUpdate
    {
        public AuthorToAddOrUpdate(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }

        public string Email { get; }
    }
}
