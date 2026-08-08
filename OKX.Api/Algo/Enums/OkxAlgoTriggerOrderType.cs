namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Trigger Order Type
/// </summary>
public enum OkxAlgoTriggerOrderType : byte
{
    /// <summary>
    /// FillOrKill
    /// </summary>
    [Map("fok")]
    FillOrKill = 1,

    /// <summary>
    /// ImmediateOrCancelOrder
    /// </summary>
    [Map("ioc")]
    ImmediateOrCancelOrder = 2,

    /// <summary>
    /// Chase limit order. Only applicable to FUTURES and SWAP trigger orders.
    /// </summary>
    [Map("chase")]
    Chase = 3,
}
