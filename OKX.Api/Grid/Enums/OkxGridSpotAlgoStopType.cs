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
    SellBaseCurrency,

    /// <summary>
    /// KeepBaseCurrency
    /// </summary>
    [Map("2")]
    KeepBaseCurrency,
}