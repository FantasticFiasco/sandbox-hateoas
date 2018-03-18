using System.ComponentModel.DataAnnotations;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class ArticleRequestBody
    {
        [Required]
        public virtual int? AuthorId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
