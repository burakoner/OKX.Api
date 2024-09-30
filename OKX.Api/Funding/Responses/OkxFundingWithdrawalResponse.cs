namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Response
/// </summary>
public record OkxFundingWithdrawalResponse
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Chain
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("wdId")]
    public long WithdrawalId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;
}