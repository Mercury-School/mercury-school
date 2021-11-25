namespace MercurySchool.Api.Repositories;

public class PersonRepository : RepositoryBase, IPersonRepository
{
    private readonly string _sqlConnectionString;
    private readonly ILogger _logger;

    public PersonRepository(IOptions<DatabaseOptions> options, ILogger<PersonRepository> logger)
    {
        _sqlConnectionString = GetSqlConnectionString(options.Value);
        _logger = logger;
    }

    public async Task<List<Person>> GetPersonsAsync(int? offset = null, int? fetch = null)
    {
        _logger.LogTrace($"{nameof(GetPersonsAsync)} called.");

        using var sqlConnection = new SqlConnection(_sqlConnectionString);
        using var sqlCommand = sqlConnection.CreateCommand();

        sqlCommand.CommandText = CreateCommandText(nameof(GetPersonsAsync));
        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue("@Offset", offset);
        sqlCommand.Parameters.AddWithValue("@fetch", fetch);

        await sqlCommand.Connection.OpenAsync();
        using var reader = await sqlCommand.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            _logger.LogTrace($"{nameof(GetPersonsAsync)} did not return any rows.");
            return null;
        }

        var persons = new List<Person>();

        while (await reader.ReadAsync())
        {
            var person = new Person
            {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"] as string,
                MiddleName = reader["MiddleName"] as string,
                LastName = reader["LastName"] as string
            };

            persons.Add(person);
        }

        _logger.LogTrace($"{nameof(GetPersonsAsync)} returned ${persons.Count} rows.");
        return persons;
    }
}
