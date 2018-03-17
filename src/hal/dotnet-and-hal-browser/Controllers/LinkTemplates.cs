using Halcyon.HAL;

namespace Hateoas.Controllers
{
    public static class LinkTemplates
    {
        public static class Author
        {
            public static Link Self { get; } = new Link("self", "/api/authors/{Id}", method: "GET");

            public static Link Create { get; } = new Link("create", "/api/authors", method: "POST", title: "Create new author");

            public static Link GetAll { get; } = new Link("authors", "/api/authors", method: "GET", title: "Get all authors");

            public static Link Edit { get; } = new Link("edit", "/api/authors/{Id}", method: "PUT", title: "Edit author");

            public static Link Delete { get; } = new Link("delete", "/api/authors/{Id}", method: "DELETE", title: "Delete author");

            public static Link AssociatedArticles { get; } = new Link("articles", "/api/authors/{Id}/articles", method: "GET", title: "Get articles by author");
        }

        public static class Article
        {
            public static Link Self { get; } = new Link("self", "/api/articles/{Id}", method: "GET");

            public static Link Create { get; } = new Link("create", "/api/articles", method: "POST", title: "Create new article");

            public static Link GetAll { get; } = new Link("articles", "/api/articles", method: "GET", title: "Get all articles");

            public static Link Comments { get; } = new Link("comments", "/api/articles/{Id}/comments", method: "GET", title: "Get comments from article");

            public static Link Edit { get; } = new Link("edit", "/api/articles/{Id}", method: "PUT", title: "Edit article");

            public static Link Delete { get; } = new Link("delete", "/api/articles/{Id}", method: "DELETE", title: "Delete article");
        }

        public static class Comment
        {
            public static Link Self { get; } = new Link("self", "/api/articles/{ArticleId}/comments/{Id}", method: "GET");

            public static Link Create { get; } = new Link("create", "/api/articles/{ArticleId}/comments", method: "POST", title: "Create new comment");

            public static Link GetAllForArticle { get; } = new Link("comments", "/api/article/{ArticleId}/comments/{Id}", method: "GET", title: "Get other comments from article");

            public static Link Delete { get; } = new Link("delete", "/api/comments/{Id}", method: "DELETE", title: "Delete comment");
        }
    }
}
