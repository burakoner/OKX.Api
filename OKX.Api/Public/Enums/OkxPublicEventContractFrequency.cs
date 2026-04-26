namespace OKX.Api.Public;

/// <summary>
/// Event contract series frequency.
/// </summary>
public enum OkxPublicEventContractFrequency : byte
{
    /// <summary>
    /// Fifteen-minute series.
    /// </summary>
    [Map("fifteen_min")]
    FifteenMinute = 1,

    /// <summary>
    /// Daily series.
    /// </summary>
    [Map("daily")]
    Daily = 2,
}
