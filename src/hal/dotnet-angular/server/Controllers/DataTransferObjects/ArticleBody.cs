using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class ArticleBody
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int? AuthorId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
