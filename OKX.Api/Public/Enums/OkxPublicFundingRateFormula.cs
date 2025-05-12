namespace OKX.Api.Public;

/// <summary>
/// OKX Funding Rate Formula
/// </summary>
public enum OkxPublicFundingRateFormula : byte
{
    /// <summary>
    /// old funding rate formula
    /// </summary>
    [Map("noRate")]
    NoRate = 1,

    /// <summary>
    /// new funding rate formula
    /// </summary>
    [Map("withRate")]
    WithRate = 2,
}