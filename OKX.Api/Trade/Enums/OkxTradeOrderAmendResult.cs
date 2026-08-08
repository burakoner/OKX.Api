namespace OKX.Api.Trade;

/// <summary>
/// Result of an order amendment reported by the orders WebSocket channel.
/// </summary>
public enum OkxTradeOrderAmendResult : sbyte
{
    /// <summary>
    /// Amendment failed.
    /// </summary>
    [Map("-1")]
    Failure = -1,

    /// <summary>
    /// Amendment succeeded.
    /// </summary>
    [Map("0")]
    Success = 0,

    /// <summary>
    /// Amendment was accepted but subsequently failed and the order was automatically canceled.
    /// </summary>
    [Map("1")]
    AutomaticCancel = 1,

    /// <summary>
    /// Automatic option price amendment succeeded.
    /// </summary>
    [Map("2")]
    AutomaticOptionPriceAmendment = 2,
}
