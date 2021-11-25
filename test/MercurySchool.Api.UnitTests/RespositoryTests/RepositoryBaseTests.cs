namespace MercurySchool.Api.UnitTests.RespositoryTests;

public class RepositoryBaseTests
{
    private readonly static string _userSecretId = "67b913c4-55fc-46b0-8739-bcb04aadea72";

    [Fact]
    public void CreateCommandTextRemovedSuffix()
    {
        // Arrange
        var methodName = "GetPersonsAsync";
        var expected = "api.GetPersons";

        // Act
        var result = RepositoryBase.CreateCommandText(methodName);

        // Assert
        _ = result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetSqlConnectionString()
    {
        // Arrange
        var databaseOptions = CreateDatabaseOptions();

        // Act
        var result = RepositoryBase.CreateSqlConnectionString(databaseOptions.Value);

        // Assert
        _ = result.Should().NotBeNull();
    }

    private static IOptions<DatabaseOptions> CreateDatabaseOptions()
    {
        var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Development.json", false, true)
                .AddUserSecrets(_userSecretId)
                .Build();

        var databaseOptions = new DatabaseOptions
        {
            DataSource = config["DataSource"],
            InitialCatalog = config["InitialCatalog"],
            UserID = config["UserID"],
            Password = config["Password"]
        };

        var options = Options.Create(databaseOptions);
        return options;
    }
}
