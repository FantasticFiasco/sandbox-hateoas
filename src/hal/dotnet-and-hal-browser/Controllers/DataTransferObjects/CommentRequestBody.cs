using System.ComponentModel.DataAnnotations;

namespace Hateoas.Controllers.DataTransferObjects
{
    public class CommentRequestBody
    {
        [Required]
        public virtual int? AuthorId { get; set; }

        [Required]
        public virtual int? ArticleId { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
