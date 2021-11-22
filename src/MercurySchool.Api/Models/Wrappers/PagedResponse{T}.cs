using System;

namespace MercurySchool.Api.Models.Wrappers;

public class PagedResponse<T> : Response<T>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public Uri FirstPage { get; init; }
    public Uri LastPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public Uri NextPage { get; init; }
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
