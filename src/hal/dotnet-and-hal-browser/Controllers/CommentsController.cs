using System.Net;
using Halcyon.Web.HAL;
using Hateoas.Controllers.DataTransferObjects;
using Hateoas.Models;
using Hateoas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hateoas.Controllers
{
    [Route("api")]
    public class CommentsController : Controller
    {
        private readonly CommentService commentService;
        private readonly AuthorService authorService;
        private readonly ArticleService articleService;

        public CommentsController(
            CommentService commentService,
            AuthorService authorService,
            ArticleService articleService)
        {
            this.commentService = commentService;
            this.authorService = authorService;
            this.articleService = articleService;
        }

        [HttpPost("articles/{articleId:int}/comments")]
        public IActionResult Post([FromBody] CommentBody body)
        {
            var comment = commentService.Add(body.MapTo<CommentToAdd>());

            return this.CreateHalResponse(comment.MapTo<CommentBody>())
                .AddLink(LinkTemplates.Comment.Self)
                .AddLocationHeader(this, comment.Id)
                .ToActionResult(this, HttpStatusCode.Created);
        }

        [HttpGet("articles/{articleId}/comments/{id:int}")]
        public IActionResult Get(int articleId, int id)
        {
            var comment = commentService.Get(id);

            if (comment == null)
            {
                return NotFound();
            }

            if (comment.ArticleId != articleId)
            {
                return BadRequest();
            }

            var author = authorService.Get(comment.AuthorId);
            var article = articleService.Get(articleId);

            return this.CreateHalResponse(comment.MapTo<CommentBody>())
                .AddLink(LinkTemplates.Comment.Self)
                .AddLink(LinkTemplates.Comment.Delete)
                .AddEmbeddedResource("author", author.MapTo<AuthorBody>(), LinkTemplates.Author.Self)
                .AddEmbeddedResource("article", article.MapTo<ArticleBody>(), LinkTemplates.Article.Self)
                .ToActionResult(this);
        }

        [HttpGet("articles/{articleId:int}/comments")]
        public IActionResult GetFromArticle(int articleId)
        {
            var comments = commentService.GetAll(articleId);

            return this.CreateHalResponse()
                .AddSelfLink(Request)
                .AddEmbeddedCollection("comments", comments.MapTo<CommentBody[]>(), LinkTemplates.Comment.Self)
                .ToActionResult(this);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool success = commentService.Remove(id);

            if (!success)
            {
                return NotFound();
            }

            return this.CreateHalResponse()
                .AddLink(LinkTemplates.Comment.GetAllForArticle)
                .ToActionResult(this);
        }
    }
}
