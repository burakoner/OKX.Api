namespace OKX.Api.Trade;

/// <summary>
/// OKX Debt Type
/// </summary>
public enum OkxTradeDebtType
{
    /// <summary>
    /// Cross
    /// </summary>
    [Map("cross")]
    Cross = 1,

    /// <summary>
    /// Isolated
    /// </summary>
    [Map("isolated")]
    Isolated = 2,
}