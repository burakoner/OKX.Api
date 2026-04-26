namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan maximum collateral redeem amount response
/// </summary>
public record OkxFinancialFlexibleLoanMaximumCollateralRedeemAmount
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Maximum redeem amount
    /// </summary>
    [JsonProperty("maxRedeemAmt"), JsonConverter(typeof(DecimalAsStringConverter))]
    public decimal MaximumRedeemAmount { get; set; }
}
