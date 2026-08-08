namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate list sort direction.
/// </summary>
public enum OkxAffiliateSortDirection : byte
{
    /// <summary>Ascending.</summary>
    [Map("asc")]
    Ascending = 0,

    /// <summary>Descending.</summary>
    [Map("desc")]
    Descending = 1,
}
