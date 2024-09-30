namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Actual Side
/// </summary>
public enum OkxAlgoActualSide
{
    /// <summary>
    /// StopLoss
    /// </summary>
    [Map("sl")]
    StopLoss = 1,

    /// <summary>
    /// TakeProfit
    /// </summary>
    [Map("tp")]
    TakeProfit = 2,
}