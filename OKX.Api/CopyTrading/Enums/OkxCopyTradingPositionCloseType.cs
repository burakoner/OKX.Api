namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX Copy Trading Position Close Type
/// </summary>
public enum OkxCopyTradingPositionCloseType : byte
{
    /// <summary>
    /// Market Close
    /// </summary>
    [Map("market_close")]
    MarketClose = 1,

    /// <summary>
    /// Copy Close
    /// </summary>
    [Map("copy_close")]
    CopyClose = 2,

    /// <summary>
    /// Manual Close
    /// </summary>
    [Map("manual_close")]
    ManualClose = 3,
}