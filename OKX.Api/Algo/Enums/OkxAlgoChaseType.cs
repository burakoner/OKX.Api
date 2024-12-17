namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Chase Type
/// </summary>
public enum OkxAlgoChaseType
{
    /// <summary>
    /// Distance from best bid/ask price, the default value
    /// </summary>
    [Map("distance")]
    Distance = 1,

    /// <summary>
    /// Ratio
    /// </summary>
    [Map("ratio")]
    Ratio = 2,
}