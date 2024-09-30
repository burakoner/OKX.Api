namespace OKX.Api.Trade;

/// <summary>
/// OKX Easy Convert Order Status
/// </summary>
public enum OkxTradeEasyConvertOrderStatus
{
    /// <summary>
    /// Running
    /// </summary>
    [Map("running")]
    Running = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 2,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed = 3,
}