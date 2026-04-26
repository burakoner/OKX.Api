namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan collateral assets response
/// </summary>
public record OkxFinancialFlexibleLoanCollateralAssets
{
    /// <summary>
    /// Available collateral assets
    /// </summary>
    [JsonProperty("assets")]
    public List<OkxFinancialFlexibleLoanAsset> Assets { get; set; } = [];
}
