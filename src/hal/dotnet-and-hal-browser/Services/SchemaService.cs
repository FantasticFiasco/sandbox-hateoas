using System;
using System.Threading.Tasks;
using NJsonSchema;

namespace Hateoas.Services
{
    public class SchemaService
    {
        public async Task<string> GetJsonSchemaAsync(Type entityType)
        {
            var schema = await JsonSchema4.FromTypeAsync(entityType);
            return schema.ToJson();
        }
    }
}
