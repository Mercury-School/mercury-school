using Microsoft.Extensions.Options;

namespace MercurySchool.Api.IntegrationTests.RepositoryTests;

public class RepositoryTestBase
{
    private readonly static string _userSecretId = "67b913c4-55fc-46b0-8739-bcb04aadea72";

    internal static IOptions<DatabaseOptions> CreateDatabaseOptions()
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
