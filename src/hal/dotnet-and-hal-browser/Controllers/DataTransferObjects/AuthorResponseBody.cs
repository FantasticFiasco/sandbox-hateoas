using Newtonsoft.Json;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class AuthorResponseBody : AuthorRequestBody
    {
        // <remarks>
        // Id should not be returned to client, but is needed internally for the HAL link
        // templates
        // </remarks>
        [JsonIgnore]
        public int Id { get; set; }
    }
}
