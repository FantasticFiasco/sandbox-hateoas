using System.ComponentModel.DataAnnotations;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class AuthorRequestBody
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
