namespace OKX.Api.Funding;

/// <summary>
/// OKX buy/sell currency lists
/// </summary>
public record OkxFundingBuySellCurrencies
{
    /// <summary>
    /// Fiat currencies available for buy/sell.
    /// </summary>
    [JsonProperty("fiatCcyList")]
    public List<OkxFundingBuySellCurrency> FiatCurrencies { get; set; } = [];

    /// <summary>
    /// Crypto currencies available for buy/sell.
    /// </summary>
    [JsonProperty("cryptoCcyList")]
    public List<OkxFundingBuySellCurrency> CryptoCurrencies { get; set; } = [];
}
