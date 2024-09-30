namespace OKX.Api.Common;

/// <summary>
/// OKX Instrument Rule Type
/// </summary>
public enum OkxInstrumentRuleType
{
    /// <summary>
    /// Normal
    /// </summary>
    [Map("normal")]
    Normal,

    /// <summary>
    /// PreMarket
    /// </summary>
    [Map("pre_market")]
    PreMarket,
}