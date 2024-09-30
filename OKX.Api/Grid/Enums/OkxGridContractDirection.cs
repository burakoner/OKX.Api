namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Contract Direction
/// </summary>
public enum OkxGridContractDirection
{
    /// <summary>
    /// Long
    /// </summary>
    [Map("long")]
    Long,

    /// <summary>
    /// Short
    /// </summary>
    [Map("short")]
    Short,

    /// <summary>
    /// Neutral
    /// </summary>
    [Map("neutral")]
    Neutral
}