using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetSchemaForPostRequest(string entityName)
        {
            if (!TypeByEntityName.TryGetValue(entityName, out var entityType))
            {
                return BadRequest();
            }

            var schema = await schemaService.GetJsonSchemaAsync(entityType);
            return Ok(schema);
        }

        [HttpGet]
        [Route("{entityName:regex(\\w+)}/{id:regex(\\d+)}/schema")]
        public async Task<IActionResult> GetSchemaForPutRequest(string entityName, int id)
        {
            if (!TypeByEntityName.TryGetValue(entityName, out var entityType))
            {
                return BadRequest();
            }

            var schema = await schemaService.GetJsonSchemaAsync(entityType);
            return Ok(schema);
        }
    }
}
