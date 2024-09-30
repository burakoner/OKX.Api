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
    Any = 0,

    /// <summary>
    /// Spot
    /// </summary>
    [Map("SPOT")]
    Spot = 1,

    /// <summary>
    /// Margin
    /// </summary>
    [Map("MARGIN")]
    Margin = 2,

    /// <summary>
    /// Swap
    /// </summary>
    [Map("SWAP")]
    Swap = 3,

    /// <summary>
    /// Futures
    /// </summary>
    [Map("FUTURES")]
    Futures = 4,

    /// <summary>
    /// Option
    /// </summary>
    [Map("OPTION")]
    Option = 5,

    /// <summary>
    /// Contracts
    /// </summary>
    [Map("CONTRACTS")]
    Contracts = 6,
}