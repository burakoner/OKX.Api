namespace OKX.Api.Funding;

/// <summary>
/// OKX Transfer Response
/// </summary>
public record OkxFundingTransferResponse
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long TransferId { get; set; }
    
    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Remitting Account
    /// </summary>
    [JsonProperty("from")]
    public OkxAccount RemittingAccount { get; set; }
    
    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Beneficiary Account
    /// </summary>
    [JsonProperty("to")]
    public OkxAccount BeneficiaryAccount { get; set; }
}
