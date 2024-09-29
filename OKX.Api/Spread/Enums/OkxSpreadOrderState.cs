namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order State
/// </summary>
public enum OkxSpreadOrderState
{
    /// <summary>
    /// Live
    /// </summary>
    Live,

    /// <summary>
    /// Filled
    /// </summary>
    Filled,

    /// <summary>
    /// Partially Filled
    /// </summary>
    PartiallyFilled,

    /// <summary>
    /// Canceled
    /// </summary>
    Canceled,
}