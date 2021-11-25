namespace MercurySchool.Api.IntegrationTests.RepositoryTests
{
    public class PersonRepositoryTests : RepositoryTestBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonRepositoryTests()
        {
            var options = CreateDatabaseOptions();
            _personRepository = new PersonRepository(options, new NullLogger<PersonRepository>());
        }

        [Theory(DisplayName = "GetPersonsAsync method should return a list of type person")]
        [InlineData(0, 10)]
        [InlineData(null, null)]
        public async Task GetPersonsAsyncShouldReturnList(int? offset, int? fetch)
        {
            // Arrange
            // Act
            var result = await _personRepository.GetPersonsAsync(offset, fetch);

            // Assert
            _ = result.Should().NotBeEmpty();
        }

        [Theory(DisplayName = "GetPersonsAsync method should return an instance of Person")]
        [InlineData(1)]
        public async Task GetPersonAsyncShouldInstanceOdPerson(int id)
        {
            // Arrange
            // Act
            var result = await _personRepository.GetPersonAsync(id);

            // Assert
            _ = result.Should().BeOfType<Person>();
        }

        [Theory(DisplayName = "InsertPersonAsync should accept parameters.")]
        [InlineData("John", "Q", "Public")]
        [InlineData("John", null, "Public")]
        public async Task InsertPersonAsyncShouldAcceptParameters(string? firstName, string? middelName, string? lastName)
        {
            // Arrange
            var person = new Person
            {
                FirstName = firstName,
                MiddleName = middelName,
                LastName = lastName
            };

            // Act
            var result = await _personRepository.InsertPersonAsync(person);

            // Assert
            _ = result.Should().BePositive();
        }
    }
}
