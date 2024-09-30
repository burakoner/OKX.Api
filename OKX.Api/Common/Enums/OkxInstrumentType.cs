namespace OKX.Api.Common;

/// <summary>
/// OKX Instrument Type
/// </summary>
public enum OkxInstrumentType
{
    /// <summary>
    /// Any
    /// </summary>
    [Map("ANY")]
    Any,

    /// <summary>
    /// Spot
    /// </summary>
    [Map("SPOT")]
    Spot,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("MARGIN")]
    Margin,

    /// <summary>
    /// Swap
    /// </summary>
    [Map("SWAP")]
    Swap,

    /// <summary>
    /// Futures
    /// </summary>
    [Map("FUTURES")]
    Futures,

    /// <summary>
    /// Option
    /// </summary>
    [Map("OPTION")]
    Option,

    /// <summary>
    /// Contracts
    /// </summary>
    [Map("CONTRACTS")]
    Contracts,
}