namespace OKX.Api.Public;

/// <summary>
/// OKX Public Data Aggregation Type
/// </summary>
public enum OkxPublicDateAggregationType : byte
{
    /// <summary>
    /// daily (not supported for module = 3 &amp; instFamilyList ≠ ANY)
    /// </summary>
    [Map("daily")]
    Daily = 1,

    /// <summary>
    /// monthly (not supported for module = 6)
    /// </summary>
    [Map("monthly")]
    Monthly = 2,
}