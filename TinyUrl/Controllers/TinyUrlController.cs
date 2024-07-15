using Microsoft.AspNetCore.Mvc;
using TinyUrl.Exceptions;
using TinyUrl.Models;
using TinyUrl.UrlShortBL;

namespace TinyUrl.Controllers
{

    [ApiController]
    [Route("/")]
    public class TinyUrlController : ControllerBase
    {
        private readonly IUrlShortning _urlShortning;
        private readonly IOriginalUrlGetter _originalUrlGetter;

        public TinyUrlController(IUrlShortning urlShortning,
            IOriginalUrlGetter originalUrlGetter)
        {
            _urlShortning = urlShortning;
            _originalUrlGetter = originalUrlGetter;
        }

        [HttpPost(Name = "GenerateShortUrl")]
        public async Task<ActionResult<Url>> Post(string originalUrl)
        {
            var resultUrl = await _urlShortning.RunAsync(originalUrl);

            var address = new Uri(Request.Host.ToString()).AbsoluteUri;
            resultUrl.ShortUrl = $"https://{address}/{resultUrl.ShortUrl}";
            return resultUrl;
        }

        [HttpGet(Name = "GetOriginalUrl")]
        [HttpGet("{shortUrl}")]
        public async Task<string> Get(string shortUrl)
        {
            var url = await _originalUrlGetter.GetOriginalUrl(shortUrl);
            if (url == null)
            {
                throw new UrlNotFoundException("Url not found, please enter another parameters");
            }
            return url.OriginalUrl;
        }

    }
}
