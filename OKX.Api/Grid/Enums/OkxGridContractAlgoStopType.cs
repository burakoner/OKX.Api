namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Contract Algo Stop Type
/// </summary>
public enum OkxGridContractAlgoStopType
{
    /// <summary>
    /// MarketCloseAllPositions
    /// </summary>
    [Map("1")]
    MarketCloseAllPositions = 1,

    /// <summary>
    /// KeepPositions
    /// </summary>
    [Map("2")]
    KeepPositions = 2
}