namespace MercurySchool.Api.Repositories;

public interface IPersonRepository
{
    /// <summary>
    /// Get a list of persons from the <c>dbo.Person</c> table
    /// </summary>
    /// <param name="offset">Page start</param>
    /// <param name="fetch">Page size</param>
    /// <returns>List of type <c>Person</c></returns>
    Task<List<Person>> GetPersonsAsync(int? offset = null, int? fetch = null);

    /// <summary>
    /// Get a person from the <c>dbo.Person</c> table
    /// </summary>
    /// <param name="id">Primary key</param>
    /// <returns>An instance of <c>Person</c></returns>
    Task<Person> GetPersonAsync(int id);
}
