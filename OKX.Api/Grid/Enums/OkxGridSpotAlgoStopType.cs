namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Spot Algo Stop Type
/// </summary>
public enum OkxGridSpotAlgoStopType
{
    /// <summary>
    /// SellBaseCurrency
    /// </summary>
    [Map("1")]
    SellBaseCurrency = 1,

    /// <summary>
    /// KeepBaseCurrency
    /// </summary>
    [Map("2")]
    KeepBaseCurrency = 2,
}