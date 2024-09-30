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
    None = 0,

    /// <summary>
    /// ManualStop
    /// </summary>
    [Map("1")]
    ManualStop = 1,

    /// <summary>
    /// TakeProfit
    /// </summary>
    [Map("2")]
    TakeProfit = 2,

    /// <summary>
    /// StopLoss
    /// </summary>
    [Map("3")]
    StopLoss = 3,

    /// <summary>
    /// RiskControl
    /// </summary>
    [Map("4")]
    RiskControl = 4,

    /// <summary>
    /// Delivery
    /// </summary>
    [Map("5")]
    Delivery = 5,

    /// <summary>
    /// Signal
    /// </summary>
    [Map("6")]
    Signal = 6,
}