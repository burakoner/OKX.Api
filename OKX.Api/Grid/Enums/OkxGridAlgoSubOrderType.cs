namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Sub-Order Type
/// </summary>
public enum OkxGridAlgoSubOrderType
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled,
}