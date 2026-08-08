namespace OKX.Api.Account;

/// <summary>
/// GLP business line.
/// </summary>
public enum OkxAccountGlpProgram : byte
{
    /// <summary>
    /// Spot.
    /// </summary>
    [Map("SPOT")]
    Spot = 1,

    /// <summary>
    /// Perpetual swaps.
    /// </summary>
    [Map("PERP")]
    Perpetual = 2,

    /// <summary>
    /// Expiry futures and Nitro.
    /// </summary>
    [Map("FUT_NTO")]
    ExpiryAndNitro = 3,
}
