namespace OKX.Api.Public;

/// <summary>
/// Event contract series frequency.
/// </summary>
public enum OkxPublicEventContractFrequency : byte
{
    /// <summary>
    /// Five-minute series.
    /// </summary>
    [Map("five_min")]
    FiveMinute = 3,

    /// <summary>
    /// Fifteen-minute series.
    /// </summary>
    [Map("fifteen_min")]
    FifteenMinute = 1,

    /// <summary>
    /// Hourly series.
    /// </summary>
    [Map("hourly")]
    Hourly = 4,

    /// <summary>
    /// Daily series.
    /// </summary>
    [Map("daily")]
    Daily = 2,

    /// <summary>
    /// Monthly series.
    /// </summary>
    [Map("monthly")]
    Monthly = 5,
}
