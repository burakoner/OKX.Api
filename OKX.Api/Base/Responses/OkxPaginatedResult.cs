namespace OKX.Api.Base;

/// <summary>
/// A page of OKX response data.
/// </summary>
/// <typeparam name="T">Item type</typeparam>
public record OkxPaginatedResult<T> where T : class
{
    /// <summary>
    /// Total number of pages under the current filters and page size.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// Items in the current page.
    /// </summary>
    public List<T> Items { get; set; } = [];
}
