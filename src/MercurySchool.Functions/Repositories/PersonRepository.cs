using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MercurySchool.Functions.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MercurySchool.Functions.Repositories
{
    /// <summary>
    /// Class that implements IPersonRepository
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly string _sqlConnectionString;

        public PersonRepository(IConfiguration configuration) => _sqlConnectionString = DataUtilities.GetSqlConnectionString(configuration);

        public async Task<List<Person>> GetPersons(SqlPagination sqlPagination) => await GetPersonsAsync(sqlPagination, null);

        public async Task<Person> GetPersonsAsync(int id)
        {
            var sqlPagination = new SqlPagination();
            var persons = await GetPersonsAsync(sqlPagination, id);

            return persons.FirstOrDefault();
        }

        public async Task<Person> InsertPersonsAsync(Person person)
        {
            using var sqlConnection = new SqlConnection(_sqlConnectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "func.InsertPerson";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@FirstName", person.FirstName);
            sqlCommand.Parameters.AddWithValue("@MiddleName", (object)person.MiddleName ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", person.LastName);

            using var reader = await sqlCommand.ExecuteReaderAsync();
            await reader.ReadAsync();

            return new Person
            {
                Id = (int?)reader["Id"],
                FirstName = reader["FirstName"] as string,
                MiddleName = reader["MiddleName"] as string,
                LastName = reader["LastName"] as string
            };
        }

        public async Task<List<Person>> InsertPersonsAsync(Queue<Person> persons) => await UpsertPersonsAsync(persons, true);

        public async Task<List<Person>> UpdatePersonsAsync(Queue<Person> persons) => await UpsertPersonsAsync(persons, false);

        public async Task<Person> UpdatePersonsAsync(Person person)
        {
            using var sqlConnection = new SqlConnection(_sqlConnectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "func.UpdatePerson";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", person.Id);
            sqlCommand.Parameters.AddWithValue("@FirstName", person.FirstName);
            sqlCommand.Parameters.AddWithValue("@MiddleName", (object)person.MiddleName ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@LastName", person.LastName);

            using var reader = await sqlCommand.ExecuteReaderAsync();

            await reader.ReadAsync();

            return new Person
            {
                Id = (int?)reader["Id"],
                FirstName = reader["FirstName"] as string,
                MiddleName = reader["MiddleName"] as string,
                LastName = reader["LastName"] as string
            };
        }

        public async Task<int> DeletePersonsAsync(int id)
        {
            using var sqlConnection = new SqlConnection(_sqlConnectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "func.DeletePerson";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            var deletedId = await sqlCommand.ExecuteScalarAsync();

            return (int)deletedId;
        }

        private async Task<List<Person>> UpsertPersonsAsync(Queue<Person> persons, bool isInsert)
        {
            var cmdText = isInsert ? "func.InsertPersons" : "func.UpdatePersons";
            var personDataTable = await GetPersonsDataTableAsync();

            while (persons.Count > 0)
            {
                var person = persons.Dequeue();
                var row = personDataTable.NewRow();

                row["FirstName"] = person.FirstName;
                row["MiddleName"] = person.MiddleName;
                row["LastName"] = person.LastName;

                personDataTable.Rows.Add(row);
            }

            using var sqlConnection = new SqlConnection(_sqlConnectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = cmdText;
            sqlCommand.CommandType = CommandType.StoredProcedure;

            var newPersons = sqlCommand.Parameters.AddWithValue("@Persons", personDataTable);
            newPersons.SqlDbType = SqlDbType.Structured;
            newPersons.TypeName = "func.Persons";

            using var reader = await sqlCommand.ExecuteReaderAsync();

            var upsertedPersons = new List<Person>();

            while (await reader.ReadAsync())
            {
                var person = new Person
                {
                    Id = (int)reader["id"],
                    FirstName = reader["FirstName"] as string,
                    MiddleName = reader["MiddleName"] as string,
                    LastName = reader["LastName"] as string
                };

                upsertedPersons.Add(person);
            }

            return upsertedPersons;
        }

        private async Task<DataTable> GetPersonsDataTableAsync(){
            
            await Task.Yield();

            var personDataTable = new DataTable("Persons");
            var idColumn = personDataTable.Columns.Add("Id", typeof(int));

            personDataTable.Columns.Add("FirstName", typeof(string));
            personDataTable.Columns.Add("MiddleName", typeof(string));
            personDataTable.Columns.Add("LastName", typeof(string));   
                    
            return personDataTable;
        }
        
        private async Task<List<Person>> GetPersonsAsync(SqlPagination sqlPagination, int? id)
        {
            var persons = new List<Person>();

            using var sqlConnection = new SqlConnection(_sqlConnectionString);
            await sqlConnection.OpenAsync();

            using var sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "func.GetPersons";
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", (object)id ?? DBNull.Value);
            sqlCommand.Parameters.AddWithValue("@Offset", sqlPagination.Offset);
            sqlCommand.Parameters.AddWithValue("@Fetch", sqlPagination.Fetch);

            using var reader = await sqlCommand.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return persons;
            }

            while (await reader.ReadAsync())
            {
                var person = new Person
                {
                    Id = (int)reader["id"],
                    FirstName = reader["FirstName"] as string,
                    MiddleName = reader["MiddleName"] as string,
                    LastName = reader["LastName"] as string
                };

                persons.Add(person);
            }

            return persons;
        }
    }
}