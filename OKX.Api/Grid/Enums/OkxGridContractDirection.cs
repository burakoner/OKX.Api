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
    Long = 1,

    /// <summary>
    /// Short
    /// </summary>
    [Map("short")]
    Short = -1,

    /// <summary>
    /// Neutral
    /// </summary>
    [Map("neutral")]
    Neutral = 0
}