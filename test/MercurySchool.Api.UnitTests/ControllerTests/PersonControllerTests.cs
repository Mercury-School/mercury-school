using MercurySchool.Api.Controllers;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace MercurySchool.Api.UnitTests.ControllerTests
{
    public class PersonControllerTests
    {


        [Fact]
        public void GetActionReturnsList()
        {
            // Arrange
            var personController = new PersonController(new NullLogger<PersonController>());

            // Act
            await personController.GetAsync();

            // Assert
        }
    }
}