namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Sub-Order Type
/// </summary>
public enum OkxGridAlgoSubOrderType : byte
{
    /// <summary>
    /// Live
    /// </summary>
    [Map("live")]
    Live = 1,

    /// <summary>
    /// Filled
    /// </summary>
    [Map("filled")]
    Filled = 2,
}