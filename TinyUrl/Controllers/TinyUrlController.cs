using Microsoft.AspNetCore.Mvc;

namespace TinyUrl.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TinyUrlController : ControllerBase
    {
        [HttpPost(Name = "GenerateShortUrl")]
        public string Post()
        {
            return "s";
        }
    }
}
