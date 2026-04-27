namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Stop-loss Mode
/// </summary>
public enum OkxDcaStopLossMode : byte
{
    /// <summary>
    /// Limit
    /// </summary>
    [Map("limit")]
    Limit = 1,

    /// <summary>
    /// Market
    /// </summary>
    [Map("market")]
    Market = 2,
}
