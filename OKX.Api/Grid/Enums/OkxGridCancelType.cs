namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Cancel Type
/// </summary>
public enum OkxGridCancelType
{
    /// <summary>
    /// None
    /// </summary>
    [Map("0")]
    None,

    /// <summary>
    /// ManualStop
    /// </summary>
    [Map("1")]
    ManualStop,

    /// <summary>
    /// TakeProfit
    /// </summary>
    [Map("2")]
    TakeProfit,

    /// <summary>
    /// StopLoss
    /// </summary>
    [Map("3")]
    StopLoss,

    /// <summary>
    /// RiskControl
    /// </summary>
    [Map("4")]
    RiskControl,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("5")]
    Delivery,

    /// <summary>
    /// Signal
    /// </summary>
    [Map("6")]
    Signal,
}