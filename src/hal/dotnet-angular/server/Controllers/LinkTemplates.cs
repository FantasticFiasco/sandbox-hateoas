using Halcyon.HAL;

namespace Hateoas.Controllers
{
    public static class LinkTemplates
    {
        public static class Author
        {
            public static Link Self { get; } = new Link("self", "/api/authors/{Id}", method: "GET");

            public static Link GetAll { get; } = new Link("authors", "/api/authors", method: "GET");

            public static Link Edit { get; } = new Link("edit", "/api/authors/{Id}", method: "PUT");

            public static Link Delete { get; } = new Link("delete", "/api/authors/{Id}", method: "DELETE");

            public static Link AssociatedArticles { get; } = new Link("articles", "/api/authors/{Id}/articles", method: "GET");
        }

        public static class Article
        {
            public static Link Self { get; } = new Link("self", "/api/articles/{Id}", method: "GET");

            public static Link GetAll { get; } = new Link("articles", "/api/articles", method: "GET");

            public static Link Comments { get; } = new Link("comments", "/api/articles/{Id}/comments", method: "GET");

            public static Link Edit { get; } = new Link("edit", "/api/articles/{Id}", method: "PUT");

            public static Link Delete { get; } = new Link("delete", "/api/articles/{Id}", method: "DELETE");
        }

        public static class Comment
        {
            public static Link Self { get; } = new Link("self", "/api/articles/{ArticleId}/comments/{Id}", method: "GET");

            public static Link GetAllForArticle { get; } = new Link("comments", "/api/article/{ArticleId}/comments/{Id}", method: "GET");

            public static Link Delete { get; } = new Link("delete", "/api/comments/{Id}", method: "DELETE");
        }
    }
}
