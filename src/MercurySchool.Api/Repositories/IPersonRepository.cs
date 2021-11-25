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

    /// <summary>
    /// <para>Insert person into the <c>dbo.Person</c> table</para>
    /// <para>Null return value indicates failed insert.</para>
    /// </summary>
    /// <param name="person">Instance of <c>Person</c></param>
    /// <returns>Newly created primary key</returns>
    Task<int?> InsertPersonAsync(Person person);
}
