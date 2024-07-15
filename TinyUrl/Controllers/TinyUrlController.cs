using Microsoft.AspNetCore.Mvc;
using TinyUrl.Exceptions;
using TinyUrl.Models;
using TinyUrl.UrlShortBL.CreateShortUrl;
using TinyUrl.UrlShortBL.UrlGetter;

namespace TinyUrl.Controllers
{

    [ApiController]
    [Route("/")]
    public class TinyUrlController : ControllerBase
    {
        private readonly IShortUrlCreator _urlShortning;
        private readonly IOriginalUrlGetter _originalUrlGetter;

        public TinyUrlController(IShortUrlCreator urlShortning,
            IOriginalUrlGetter originalUrlGetter)
        {
            _urlShortning = urlShortning;
            _originalUrlGetter = originalUrlGetter;
        }

        [HttpPost(Name = "GenerateShortUrl")]
        public async Task<ActionResult<Url>> CreateShortUrl(string originalUrl)
        {
            var resultUrl = await _urlShortning.RunAsync(originalUrl);

            var address = new Uri(Request.Host.ToString()).AbsoluteUri;
            resultUrl.ShortUrl = $"https://{address}/{resultUrl.ShortUrl}";
            return resultUrl;
        }

        [HttpGet(Name = "GetOriginalUrl")]
        [HttpGet("{shortUrl}")]
        public async Task RedirectToOriginalUrl(string shortUrl)
        {
            var url = await _originalUrlGetter.GetOriginalUrl(shortUrl);
            if (url == null)
            {
                throw new UrlNotFoundException("Url not found, please enter another parameters");
            }
            Response.Redirect(url.OriginalUrl);
        }
    }
}
