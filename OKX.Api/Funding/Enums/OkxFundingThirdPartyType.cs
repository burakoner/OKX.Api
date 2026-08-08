namespace OKX.Api.Funding;

/// <summary>
/// Third-party custody provider used to filter funding bills
/// </summary>
public enum OkxFundingThirdPartyType : byte
{
    /// <summary>
    /// Copper
    /// </summary>
    [Map("1")]
    Copper = 1,

    /// <summary>
    /// Komainu
    /// </summary>
    [Map("2")]
    Komainu = 2,

    /// <summary>
    /// SCB
    /// </summary>
    [Map("5")]
    Scb = 5,
}
