namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order State
/// </summary>
public enum OkxSpreadOrderState
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

    /// <summary>
    /// Partially Filled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled,
}