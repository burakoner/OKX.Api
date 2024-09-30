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
    MarketCloseAllPositions,

    /// <summary>
    /// KeepPositions
    /// </summary>
    [Map("2")]
    KeepPositions
}