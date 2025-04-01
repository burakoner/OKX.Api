namespace OKX.Api.Account;

/// <summary>
/// OkxAccountCollateralAsset
/// </summary>
public record OkxAccountCollateralAsset
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Whether or not to be a collateral asset
    /// </summary>
    [JsonProperty("collateralEnabled"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool CollateralEnabled { get; set; }

}
