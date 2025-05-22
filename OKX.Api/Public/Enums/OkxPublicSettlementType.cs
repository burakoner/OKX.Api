namespace OKX.Api.Public;

/// <summary>
/// OKX Settlement Type
/// </summary>
public enum OkxPublicSettlementType : byte
{
    /// <summary>
    /// Futures settlement
    /// </summary>
    [Map("settlement")]
    FuturesSettlement = 1,

    /// <summary>
    /// Futures delivery
    /// </summary>
    [Map("delivery")]
    FuturesDelivery = 2,

    /// <summary>
    /// Option exercise
    /// </summary>
    [Map("exercise")]
    OptionXxercise = 3,
}