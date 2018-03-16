using System;
using System.Collections.Generic;
using Hateoas.Controllers.DataTransferObjects;
using Hateoas.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hateoas.Controllers
{
    [Route("api")]
    public class SchemasController : ControllerBase
    {
        private static readonly IDictionary<string, Type> TypeByEntityName;

        private readonly SchemaService schemaService;

        static SchemasController()
        {
            TypeByEntityName = new Dictionary<string, Type>
            {
                ["authors"] = typeof(AuthorRequestBody),
                ["articles"] = typeof(ArticleRequestBody),
                ["comments"] = typeof(CommentRequestBody)
            };
        }

        public SchemasController(SchemaService schemaService)
        {
            this.schemaService = schemaService;
        }

        [HttpGet]
        [Route("{entityName:regex(\\w+)}/schema")]
        public IActionResult Get(string entityName)
        {
            if (!TypeByEntityName.TryGetValue(entityName, out var entityType))
            {
                return BadRequest();
            }

            return Ok(schemaService.GetJsonSchema(entityType));
        }
    }
}
