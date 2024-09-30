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
    Running,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,
}