using Microsoft.AspNetCore.Mvc;
using TinyUrl.DB;
using TinyUrl.Models;

namespace TinyUrl.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TinyUrlController : ControllerBase
    {
        private readonly UrlsService _urlService;
        public TinyUrlController(UrlsService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost(Name = "GenerateShortUrl")]
        public async Task<ActionResult<Url>> Post(Url url)
        {

            return await _urlService.PostUrlShort(url);

        }
    }
}
