namespace OKX.Api.Account;

/// <summary>
/// OkxAccountCollateralAssets
/// </summary>
public record OkxAccountCollateralAssets
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxAccountCollateralAssetsMode Type { get; set; }

    /// <summary>
    /// Whether or not set the assets to be collateral
    /// true: Set to be collateral
    /// false: Set to be non-collateral
    /// </summary>
    [JsonProperty("collateralEnabled"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool CollateralEnabled { get; set; }

    /// <summary>
    /// Currency list, e.g. ["BTC","ETH"]
    /// </summary>
    [JsonProperty("ccyList")]
    public List<string> Currencies { get; set; } = [];
}
