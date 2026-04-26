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
    /// PreMarket
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
    /// XPerp
    /// Perpetual-style futures contract.
    /// </summary>
    [Map("xperp")]
    XPerp = 4,
}
