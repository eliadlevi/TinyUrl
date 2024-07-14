using Microsoft.AspNetCore.Mvc;
using TinyUrl.Models;
using TinyUrl.UrlShortBL;

namespace TinyUrl.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TinyUrlController : ControllerBase
    {
        private readonly IUrlShortning _urlShortning;
        public TinyUrlController(IUrlShortning urlShortning)
        {
            _urlShortning = urlShortning;
        }

        [HttpPost(Name = "GenerateShortUrl")]
        public async Task<ActionResult<Url>> Post(string originalUrl)
        {

            return await _urlShortning.RunAsync(originalUrl);

        }
    }
}
