using System.Net;
using Halcyon.Web.HAL;
using Hateoas.Controllers.DataTransferObjects;
using Hateoas.Models;
using Hateoas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hateoas.Controllers
{
    [Route("api")]
    public class ArticlesController : Controller
    {
        private readonly AuthorService authorService;
        private readonly ArticleService articleService;
        private readonly CommentService commentService;

        public ArticlesController(
            AuthorService authorService,
            ArticleService articleService,
            CommentService commentService)
        {
            this.authorService = authorService;
            this.articleService = articleService;
            this.commentService = commentService;
        }

        [HttpPost("articles")]
        public IActionResult Post([FromBody] ArticleBody body)
        {
            var article = articleService.Add(body.MapTo<ArticleToAddOrUpdate>());

            return this.CreateHalResponse(article.MapTo<ArticleBody>())
                .AddLink(LinkTemplates.Article.Self)
                .AddLocationHeader(this, article.Id)
                .ToActionResult(this, HttpStatusCode.Created);
        }

        [HttpGet("articles/{id:int}")]
        public IActionResult Get(int id)
        {
            var article = articleService.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            var author = authorService.Get(article.AuthorId);

            return this.CreateHalResponse(article.MapTo<ArticleBody>())
                .AddLink(LinkTemplates.Article.Self)
                .AddLink(LinkTemplates.Article.Edit)
                .AddLink(LinkTemplates.Article.Delete)
                .AddLink(LinkTemplates.Article.Comments)
                .AddEmbeddedResource("author", author.MapTo<AuthorBody>(), LinkTemplates.Author.Self)
                .ToActionResult(this);
        }

        [HttpGet("authors/{authorId:int}/articles")]
        public IActionResult GetByAuthor(int authorId)
        {
            var articles = articleService.GetByAuthor(authorId);

            return this.CreateHalResponse()
                .AddSelfLink(Request)
                .AddEmbeddedCollection("articles", articles.MapTo<ArticleBody[]>(), LinkTemplates.Article.Self)
                .ToActionResult(this);
        }

        [HttpGet("articles")]
        public IActionResult GetAll()
        {
            var articles = articleService.GetAll();

            return this.CreateHalResponse()
                .AddSelfLink(Request)
                .AddLink(LinkTemplates.Article.Create)
                .AddEmbeddedCollection("articles", articles.MapTo<ArticleBody[]>(), LinkTemplates.Article.Self)
                .ToActionResult(this);
        }

        [HttpPut("articles/{id:int}")]
        public IActionResult Put(int id, [FromBody] ArticleBody body)
        {
            var article = articleService.Update(id, body.MapTo<ArticleToAddOrUpdate>());

            if (article == null)
            {
                return NotFound();
            }

            return this.CreateHalResponse(article.MapTo<ArticleBody>())
                .AddLink(LinkTemplates.Article.Self)
                .ToActionResult(this);
        }

        [HttpDelete("articles/{id:int}")]
        public IActionResult Delete(int id)
        {
            var article = articleService.Get(id);

            if (article == null)
            {
                return NotFound();
            }

            // Remove article
            articleService.Remove(id);

            // Remove comments
            foreach (var comment in commentService.GetAll(article.Id))
            {
                commentService.Remove(comment.Id);
            }

            return this.CreateHalResponse()
                .AddLink(LinkTemplates.Article.GetAll)
                .ToActionResult(this);
        }
    }
}
