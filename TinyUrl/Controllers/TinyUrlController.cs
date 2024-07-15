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
            resultUrl.ShortUrl = $"{address}/{resultUrl.ShortUrl}";
            return resultUrl;
        }

        //TODO: add method to send a short url and get the orignal url.
        [HttpGet(Name = "GetOriginalUrl")]
        public async Task<string> Get(string shortUrl)
        {
            var url = await _originalUrlGetter.GetOriginalUrl(shortUrl);
            return url.OriginalUrl;
        }

    }
}
