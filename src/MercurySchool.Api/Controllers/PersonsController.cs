using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercurySchool.Api.Models;
using MercurySchool.Api.Models.Filters;
using MercurySchool.Api.Models.Wrappers;
using MercurySchool.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MercurySchool.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPersonRepository _personRepository;

        public PersonsController(ILogger<PersonsController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        /// <summary>
        /// Returns as list of type Person.
        /// </summary>
        /// <returns><c>List<Person></c></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationFilter filter)
        {
            try
            {
                var persons = await _personRepository.GetPersonsAsync();
                var pagedResponse = new PagedResponse<List<Person>>(persons, 1, 25);

                if (persons is not null)
                {
                    _logger.LogTrace("returned ok.");
                    return Ok(pagedResponse);
                }

                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }

        }
    }
}