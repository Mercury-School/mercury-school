namespace MercurySchool.Api.Controllers;

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
    public async Task<IActionResult> GetAsync([FromQuery] PaginationFilter paginationFilter)
    {
        try
        {
            var persons = await _personRepository.GetPersonsAsync();
            var pagedResponse = new PagedResponse<List<Person>>(persons, paginationFilter.Offset, paginationFilter.Fetch);

            if (persons is not null)
            {
                _logger.LogTrace("returned ok.");
                return Ok(pagedResponse);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unable to retrieve data.");
            return StatusCode(500);
        }

    }
}
