namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Order Status
/// </summary>
public enum OkxTradeOneClickRepayOrderStatus
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