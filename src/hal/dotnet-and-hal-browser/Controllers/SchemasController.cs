using System;
using System.Collections.Concurrent;
using Hateoas.Controllers.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Hateoas.Controllers
{
    [Route("api")]
    public class SchemasController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, JSchema> SchemaByEntityName;

        private readonly JSchemaGenerator schemaGenerator;

        static SchemasController()
        {
            SchemaByEntityName = new ConcurrentDictionary<string, JSchema>();
        }

        public SchemasController()
        {
            schemaGenerator = new JSchemaGenerator();
        }
        
        [HttpGet]
        [Route("{entityName:regex(\\w+)}/schema")]
        public JSchema Get(string entityName)
        {
            return SchemaByEntityName.GetOrAdd(entityName, GenerateSchema);
        }

        private JSchema GenerateSchema(string entityName)
        {
            switch (entityName)
            {
                case "authors":
                    return schemaGenerator.Generate(typeof(AuthorBody));

                case "articles":
                    return schemaGenerator.Generate(typeof(ArticleBody));

                case "comments":
                    return schemaGenerator.Generate(typeof(CommentBody));

                default:
                    throw new NotSupportedException($"Schema for entity with name '{entityName}' is not supported");
            }
        }
    }
}
