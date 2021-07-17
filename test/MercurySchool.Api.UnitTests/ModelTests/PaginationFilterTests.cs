using MercurySchool.Api.Models.Filters;
using FluentAssertions;
using Xunit;

namespace MercurySchool.Api.UnitTests.ModelTests
{
    public class PaginationFilterTests
    {
        [Theory(DisplayName = "PaginationFilter should return paging values.")]
        [InlineData(1, 25)]
        [InlineData(2, 50)]
        public void PaginationFilterReturnsPagingValues(int pageNumber, int pageSize)
        {
            // Arrange
            var paginationFilter = new PaginationFilter(pageNumber, pageSize);

            // Act

            // Assert
            paginationFilter.PageNumber.Should().Be(pageNumber, $"PageNumber size is not { pageNumber }.");
            paginationFilter.PageSize.Should().Be(pageSize, $"PageSize is not { pageSize }.");
        }
    }
}