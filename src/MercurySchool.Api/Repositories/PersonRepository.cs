using System;

namespace MercurySchool.Api.Repositories;

public class PersonRepository : RepositoryBase, IPersonRepository
{
    private readonly string _sqlConnectionString;
    private readonly ILogger _logger;

    public PersonRepository(IOptions<DatabaseOptions> options, ILogger<PersonRepository> logger)
    {
        _sqlConnectionString = CreateSqlConnectionString(options.Value);
        _logger = logger;
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        _logger.LogTrace($"{nameof(GetPersonAsync)} called.");

        using var sqlConnection = new SqlConnection(_sqlConnectionString);
        using var sqlCommand = sqlConnection.CreateCommand();

        sqlCommand.CommandText = CreateCommandText(nameof(GetPersonAsync));
        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

        sqlCommand.Parameters.AddWithValue("@Id", id);

        await sqlCommand.Connection.OpenAsync();
        using var reader = await sqlCommand.ExecuteReaderAsync();

        if (!reader.HasRows)
        {
            _logger.LogTrace($"Given the id {id}, {nameof(GetPersonAsync)} did not find any data.");
            return null;
        }

        await reader.ReadAsync();

        var person = new Person
        {
            Id = (int)reader["Id"],
            FirstName = reader["FirstName"] as string,
            MiddleName = reader["MiddleName"] as string,
            LastName = reader["LastName"] as string
        };

        return person;
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

    public async Task<int?> InsertPersonAsync(Person person)
    {
        _logger.LogTrace($"{nameof(InsertPersonAsync)} called.");

        using var sqlConnection = new SqlConnection(_sqlConnectionString);
        using var sqlCommand = sqlConnection.CreateCommand();

        sqlCommand.CommandText = CreateCommandText(nameof(InsertPersonAsync));
        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

        _ = sqlCommand.Parameters.AddWithValue("@FirstName", person.FirstName);
        _ = sqlCommand.Parameters.AddWithValue("@MiddleName", person.MiddleName);
        _ = sqlCommand.Parameters.AddWithValue("@LastName", person.LastName);

        var idParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        sqlCommand.Parameters.Add(idParameter);

        await sqlCommand.Connection.OpenAsync();
        _ = await sqlCommand.ExecuteNonQueryAsync();

        return idParameter.Value == DBNull.Value ? null : (int?)idParameter.Value;
    }
}
