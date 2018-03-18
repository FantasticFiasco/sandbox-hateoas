using Newtonsoft.Json;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class ArticleResponseBody : ArticleRequestBody
    {
        // <remarks>
        // Id should not be returned to client, but is needed internally for the HAL link
        // templates.
        // </remarks>
        [JsonIgnore]
        public int Id { get; set; }

        // <remarks>
        // Id should not be returned to client, but is needed internally for the HAL link
        // templates.
        // </remarks>
        [JsonIgnore]
        public override int? AuthorId { get; set; }
    }
}
