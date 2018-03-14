using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;

namespace Hateoas.Controllers
{
    [Route("api")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.CreateHalResponse()
                .AddSelfLink(Request)
                .AddLink(LinkTemplates.Author.GetAll)
                .AddLink(LinkTemplates.Article.GetAll)
                .ToActionResult(this);
        }
    }
}
