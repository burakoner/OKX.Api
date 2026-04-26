namespace OKX.Api.Financial;

/// <summary>
/// OKX Dual Investment Trade
/// </summary>
public record OkxFinancialDualInvestmentTrade
{
    /// <summary>
    /// Quote ID
    /// </summary>
    [JsonProperty("quoteId")]
    public string QuoteId { get; set; } = string.Empty;

    /// <summary>
    /// Order ID
    /// </summary>
    [JsonProperty("ordId")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Order state
    /// </summary>
    [JsonProperty("state")]
    public OkxFinancialDualInvestmentOrderState State { get; set; }
}
