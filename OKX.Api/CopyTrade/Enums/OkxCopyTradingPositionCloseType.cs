namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX Copy Trading Position Close Type
/// </summary>
public enum OkxCopyTradingPositionCloseType
{
    /// <summary>
    /// Market Close
    /// </summary>
    [Map("market_close")]
    MarketClose,

    /// <summary>
    /// Copy Close
    /// </summary>
    [Map("copy_close")]
    CopyClose,

    /// <summary>
    /// Manual Close
    /// </summary>
    [Map("manual_close")]
    ManualClose,
}