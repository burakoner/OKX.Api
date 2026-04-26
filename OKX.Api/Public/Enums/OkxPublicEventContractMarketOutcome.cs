namespace OKX.Api.Public;

/// <summary>
/// Event contract market outcome.
/// </summary>
public enum OkxPublicEventContractMarketOutcome : byte
{
    /// <summary>
    /// Outcome not available.
    /// </summary>
    [Map("0")]
    NotAvailable = 0,

    /// <summary>
    /// YES.
    /// </summary>
    [Map("1")]
    Yes = 1,

    /// <summary>
    /// NO.
    /// </summary>
    [Map("2")]
    No = 2,
}
