using System.Collections.Generic;
using System.Threading.Tasks;
using MercurySchool.Api.Models;

namespace MercurySchool.Api.Repositories
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPersonsAsync();
    }
}