namespace OKX.Api.Account;

/// <summary>
/// OkxMaximumAvailableAmount
/// </summary>
public record OkxAccountMaximumAvailableAmount
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Amount available to buy
    /// </summary>
    [JsonProperty("availBuy")]
    public decimal AvailableBuy { get; set; }

    /// <summary>
    /// Amount available to sell
    /// </summary>
    [JsonProperty("availSell")]
    public decimal AvailableSell { get; set; }
}
