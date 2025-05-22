namespace OKX.Api.Spread;

/// <summary>
/// OKX Spread Instrument Type
/// </summary>
public enum OkxSpreadInstrumentType : byte
{
    /// <summary>
    /// Linear
    /// </summary>
    [Map("linear")]
    Linear = 1,

    /// <summary>
    /// Inverse
    /// </summary>
    [Map("inverse")]
    Inverse = 2,

    /// <summary>
    /// Hybrid
    /// </summary>
    [Map("hybrid")]
    Hybrid = 3,
}