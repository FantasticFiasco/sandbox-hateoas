using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class CommentBody
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int? AuthorId { get; set; }

        [JsonIgnore]
        public int? ArticleId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
