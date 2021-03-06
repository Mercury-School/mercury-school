namespace MercurySchool.Api.Models.Filters;

public class PaginationFilter
{
    /// <summary>
    /// Number of pages results are divided into.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Item limit per page.
    /// </summary>
    public int PageSize { get; set; } = 25;

    /// <summary>
    /// Value passed to Offset paraemter in stored procedure.
    /// </summary>
    public int Offset => PageNumber * PageSize;

    /// <summary>
    /// Value passed to Fetch paraemter in stored procedure.
    /// </summary>
    public int Fetch => PageSize;

    public PaginationFilter(int pageNumber = 1, int pageSize = 25)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize > 50 ? 50 : pageSize;
    }
}