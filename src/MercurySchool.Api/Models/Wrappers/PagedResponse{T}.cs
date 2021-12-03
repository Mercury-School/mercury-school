namespace MercurySchool.Api.Models.Wrappers;

public class PagedResponse<T> : Response<T>
{
    /// <summary>
    /// Current page number in persons dataset.
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// The number of rows to fetch from the database.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// First page in dataset.
    /// </summary>
    public Uri FirstPage { get; init; }

    /// <summary>
    /// Last page in dataset.
    /// </summary>
    public Uri LastPage { get; init; }

    /// <summary>
    /// Total number of pages in dataset based on page size.
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Total number of rows available in database table.
    /// </summary>
    public int TotalRecords { get; init; }

    /// <summary>
    /// Net page in data set.
    /// </summary>
    public Uri NextPage { get; init; }

    /// <summary>
    /// Previous page in dataset.
    /// </summary>
    public Uri PreviousPage { get; init; }

    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
        Message = null;
        Succeeded = true;
        Errors = null;
    }
}
