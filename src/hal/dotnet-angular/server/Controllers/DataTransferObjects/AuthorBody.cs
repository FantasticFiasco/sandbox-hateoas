using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class AuthorBody
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
