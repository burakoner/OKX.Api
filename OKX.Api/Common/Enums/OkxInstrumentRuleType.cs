namespace OKX.Api.Common;

/// <summary>
/// OKX Instrument Rule Type
/// </summary>
public enum OkxInstrumentRuleType : byte
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("normal")]
    Normal = 1,

    /// <summary>
    /// Pre-market trading.
    /// For FUTURES, this identifies a Pre-market X-Perp before it converts to a normal X-Perp.
    /// </summary>
    [Map("pre_market")]
    PreMarket = 2,

    /// <summary>
    /// RebaseContract
    /// Pre-market rebase contract.
    /// </summary>
    [Map("rebase_contract")]
    RebaseContract = 3,

    /// <summary>
    /// Perpetual-style FUTURES contract.
    /// A Pre-market X-Perp changes from PreMarket to XPerp after conversion.
    /// </summary>
    [Map("xperp")]
    XPerp = 4,
}
