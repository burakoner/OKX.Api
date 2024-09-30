namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Instrument Type
/// </summary>
public enum OkxSpreadInstrumentType
{
    /// <summary>
    /// Linear
    /// </summary>
    [Map("linear")]
    Linear,

    /// <summary>
    /// Inverse
    /// </summary>
    [Map("inverse")]
    Inverse,

    /// <summary>
    /// Hybrid
    /// </summary>
    [Map("hybrid")]
    Hybrid,
}