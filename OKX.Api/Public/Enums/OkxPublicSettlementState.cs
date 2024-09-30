namespace OKX.Api.Public;

/// <summary>
/// OKX Settlement State
/// </summary>
public enum OkxPublicSettlementState
{
    /// <summary>
    /// Processing
    /// </summary>
    [Map("processing")]
    Processing,

    /// <summary>
    /// Settled
    /// </summary>
    [Map("settled")]
    Settled,
}