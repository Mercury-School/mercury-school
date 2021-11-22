namespace MercurySchool.Api.Repositories;

public interface IPersonRepository
{
    Task<List<Person>> GetPersonsAsync();
}
