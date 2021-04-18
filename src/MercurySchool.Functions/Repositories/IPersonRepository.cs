using System.Collections.Generic;
using System.Threading.Tasks;
using MercurySchool.Functions.Models;

namespace MercurySchool.Functions.Repositories
{
    /// <summary>
    /// Person Repository Interface
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Rerturn a paginated list of persons.
        /// </summary>
        /// <param name="sqlPagination">Object defineing pagination.</param>
        /// <returns>List of type person</returns>
        Task<List<Person>> GetPersons(SqlPagination sqlPagination);

        /// <summary>
        /// Return an instance of person
        /// </summary>
        /// <param name="id">Primary key of person.</param>
        /// <returns>An instance of Person</returns>
        Task<Person> GetPersonsAsync(int id);

        /// <summary>
        /// Insert instance of person.
        /// </summary>
        /// <param name="person">Instance of Person</param>
        /// <returns>Instance of Person</returns>
        Task<Person> InsertPersonsAsync(Person person);

        /// <summary>
        /// Insert persons from an instance of an Queue of type person.
        /// </summary>
        /// <param name="person">Queue of type person</param>
        /// <returns>List of type person</returns>
        Task<List<Person>> InsertPersonsAsync(Queue<Person> persons);

        /// <summary>
        /// Update persons from an instance of an Queue of type person.
        /// </summary>
        /// <param name="persons">Queue of type person</param>
        /// <returns>List of type person</returns>
        Task<List<Person>> UpdatePersonsAsync(Queue<Person> persons);

        /// <summary>
        /// Update instance of person.
        /// </summary>
        /// <param name="person">Instance of Person</param>
        /// <returns>Instance of Person</returns>
        Task<Person> UpdatePersonsAsync(Person person);

        /// <summary>
        /// Delete person.
        /// </summary>
        /// <param name="id">Id of person to delete</param>
        /// <returns>Id of deleted person</returns>
        Task<int> DeletePersonsAsync(int id);
    }
}