using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MercurySchool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger _logger;
        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> GetAsync()
        {

            await Task.Yield();

            _logger.LogTrace("returned ok.")
            return Ok();
        }
    }
}
