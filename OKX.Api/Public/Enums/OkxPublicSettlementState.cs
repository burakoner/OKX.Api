namespace OKX.Api.Public;

/// <summary>
/// OKX Settlement State
/// </summary>
public enum OkxPublicSettlementState : byte
{
    /// <summary>
    /// Processing
    /// </summary>
    [Map("processing")]
    Processing = 1,

    /// <summary>
    /// Settled
    /// </summary>
    [Map("settled")]
    Settled = 2,
}