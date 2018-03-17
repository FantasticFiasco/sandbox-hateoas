﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hateoas.Controllers.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema;

namespace Hateoas.Controllers
{
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
        [Route("{entityName:regex(\\w+)}/schema")]
        public async Task<IActionResult> GetSchemaForPostRequest(string entityName)
        {
            if (!TypeByEntityName.TryGetValue(entityName, out var entityType))
            {
                return BadRequest();
            }

            var schema = await JsonSchema4.FromTypeAsync(entityType);
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

            var schema = await JsonSchema4.FromTypeAsync(entityType);
            return Ok(schema);
        }
    }
}
