namespace MercurySchool.Api.UnitTests.ControllerTests;

public class PersonsControllerTests
{
    [Fact(DisplayName = "GetAsync should return OkObjectResult.")]
    public async Task GetAsyncActionReturnsOkObjectResult()
    {
        // Arrange
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(x => x.GetPersonsAsync()).ReturnsAsync(new List<Person>());

        var personController = new PersonsController(new NullLogger<PersonsController>(), personRepository.Object);

        // Act
        var sut = await personController.GetAsync(DefaultPaginationFilter()) as OkObjectResult;

        // Assert
        personRepository.Verify(x => x.GetPersonsAsync());

        sut.Should().BeOfType<OkObjectResult>("system under test is not an object of type OkObjectResult.");

        sut.Value.Should()
            .NotBeNull("value returned is null.")
            .And.BeOfType<PagedResponse<List<Person>>>("value should be a list of type Person.");
    }

    [Fact(DisplayName = "GetAsync should return NoContentResult.")]
    public async Task GetAsyncActionReturnsNoContentResult()
    {
        // Arrange
        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(x => x.GetPersonsAsync()).ReturnsAsync((List<Person>)null);

        var personController = new PersonsController(new NullLogger<PersonsController>(), personRepository.Object);

        // Act
        var sut = await personController.GetAsync(DefaultPaginationFilter()) as NoContentResult;

        // Assert
        sut.Should().BeOfType<NoContentResult>("system under test is not an object of type NoContentResult.");
    }

    private static PaginationFilter DefaultPaginationFilter() => new PaginationFilter(1, 25);
}
