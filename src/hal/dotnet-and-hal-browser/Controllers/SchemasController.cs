using Hateoas.Controllers.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Hateoas.Controllers
{
    public class SchemasController : ControllerBase
    {
        [HttpGet]
        [Route("{entity:regex(.*)}/schema")]
        public string Get(string entity)
        {
            JSchemaGenerator generator = new JSchemaGenerator();

            switch (entity)
            {
                case "author":
                    JSchema schema = generator.Generate(typeof(AuthorBody));
                    return schema.ToString();
            }

            return entity;
        }
    }
}
