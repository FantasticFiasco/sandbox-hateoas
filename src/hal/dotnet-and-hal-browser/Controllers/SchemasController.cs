using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hateoas.Controllers.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema;

namespace Hateoas.Controllers
{
    /// <summary>
    /// Controller supporting the web client by providing JSON Schemas for types that can be
    /// created or updated by the client.
    /// </summary>
    [Route("api")]
    public class SchemasController : ControllerBase
    {
        private static readonly IDictionary<string, Type> TypeByEntityName;

        static SchemasController()
        {
            TypeByEntityName = new Dictionary<string, Type>
            {
                ["authors"] = typeof(AuthorRequestBody),
                ["articles"] = typeof(ArticleRequestBody),
                ["comments"] = typeof(CommentRequestBody)
            };
        }

        [HttpGet]
        [Route("{entityName}/schema")]
        public IActionResult GetSchemaForPostRequest(string entityName)
        {
            if (!TypeByEntityName.TryGetValue(entityName, out var entityType))
            {
                return BadRequest();
            }

            var schema = JsonSchema.FromType(entityType);
            return Ok(schema);
        }

        [HttpGet]
        [Route("{entityName}/{id:int}/schema")]
        public IActionResult GetSchemaForPutRequest(string entityName, int id)
        {
            if (!TypeByEntityName.TryGetValue(entityName, out var entityType))
            {
                return BadRequest();
            }

            var schema = JsonSchema.FromType(entityType);
            return Ok(schema);
        }
    }
}
