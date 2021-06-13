using System.Collections.Generic;
using System.Threading.Tasks;
using MercurySchool.Api.Models;

namespace MercurySchool.Api.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        public Task<List<Person>> GetPersonsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}