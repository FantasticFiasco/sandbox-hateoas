using System;
using System.Collections.Concurrent;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Hateoas.Services
{
    public class SchemaService
    {
        private readonly ConcurrentDictionary<Type, JSchema> schemaByType;
        private readonly JSchemaGenerator schemaGenerator;

        public SchemaService()
        {
            schemaByType = new ConcurrentDictionary<Type, JSchema>();
            schemaGenerator = new JSchemaGenerator();
        }

        public JSchema GetJsonSchema(Type entityType)
        {
            return schemaByType.GetOrAdd(entityType, schemaGenerator.Generate(entityType));
        }
    }
}
