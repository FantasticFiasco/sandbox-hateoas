﻿using System.Net;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Hateoas.Controllers.DataTransferObjects;
using Hateoas.Models;
using Hateoas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hateoas.Controllers
{
    [Route("api")]
    public class AuthorsController : Controller
    {
        private readonly AuthorService authorService;
        private readonly ArticleService articleService;

        public AuthorsController(
            AuthorService authorService,
            ArticleService articleService)
        {
            this.authorService = authorService;
            this.articleService = articleService;
        }

        [HttpPost("authors")]
        public IActionResult Post([FromBody] AuthorBody body)
        {
            var author = authorService.Add(body.MapTo<AuthorToAddOrUpdate>());

            return new HALResponse(author.MapTo<AuthorBody>())
                .AddLink(LinkTemplates.Author.Self)
                .AddLocationHeader(this, author.Id)
                .ToActionResult(this, HttpStatusCode.Created);
        }

        [HttpGet("authors/{id:int}")]
        public IActionResult Get(int id)
        {
            var author = authorService.Get(id);

            if (author == null)
            {
                return NotFound();
            }

            return new HALResponse(author.MapTo<AuthorBody>())
                .AddLink(LinkTemplates.Author.Self)
                .AddLink(LinkTemplates.Author.Edit)
                .AddLink(LinkTemplates.Author.Delete)
                .AddLink(LinkTemplates.Author.AssociatedArticles)
                .ToActionResult(this);
        }

        [HttpGet("authors")]
        public IActionResult GetAll()
        {
            var authors = authorService.GetAll();

            return new HALResponse(null)
                .AddSelfLink(Request)
                .AddEmbeddedCollection("authors", authors.MapTo<AuthorBody[]>(), LinkTemplates.Author.Self)
                .ToActionResult(this);
        }

        [HttpPut("authors/{id:int}")]
        public IActionResult Put(int id, [FromBody] AuthorBody body)
        {
            var author = authorService.Update(id, body.MapTo<AuthorToAddOrUpdate>());

            if (author == null)
            {
                return NotFound();
            }

            return new HALResponse(author.MapTo<AuthorBody>())
                .AddLink(LinkTemplates.Author.Self)
                .ToActionResult(this);
        }

        [HttpDelete("authors/{id:int}")]
        public IActionResult Delete(int id)
        {
            var author = authorService.Get(id);

            if (author == null)
            {
                return NotFound();
            }

            // Remove author
            authorService.Remove(author.Id);

            // Remove articles
            foreach (var article in articleService.GetByAuthor(author.Id))
            {
                articleService.Remove(article.Id);
            }

            return new HALResponse(null)
                .AddLink(LinkTemplates.Author.GetAll)
                .ToActionResult(this);
        }
    }
}
