namespace OKX.Api.Funding;

/// <summary>
/// OKX fiat payment method
/// </summary>
public record OkxFundingFiatPaymentMethod
{
    /// <summary>
    /// Fiat currency served by the payment method.
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Payment method code.
    /// </summary>
    [JsonProperty("paymentMethod")]
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    /// Fee rate applied by the payment method.
    /// </summary>
    [JsonProperty("feeRate")]
    public decimal FeeRate { get; set; }

    /// <summary>
    /// Minimum fee charged by the payment method.
    /// </summary>
    [JsonProperty("minFee")]
    public decimal MinimumFee { get; set; }

    /// <summary>
    /// Limits associated with the payment method.
    /// </summary>
    [JsonProperty("limits")]
    public OkxFundingFiatLimits Limits { get; set; } = new();

    /// <summary>
    /// Linked accounts that can be used with the payment method.
    /// </summary>
    [JsonProperty("accounts")]
    public List<OkxFundingFiatPaymentMethodAccount> Accounts { get; set; } = [];
}
