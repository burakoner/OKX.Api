namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Order State
/// </summary>
public enum OkxSpreadOrderState : byte
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

    /// <summary>
    /// Partially Filled
    /// </summary>
    [Map("partially_filled")]
    PartiallyFilled = 3,

    /// <summary>
    /// Canceled
    /// </summary>
    [Map("canceled")]
    Canceled = 4,
}