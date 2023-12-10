namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxMaximumAmount
/// </summary>
public class OkxMaximumAmount
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// SPOT/MARGIN: The maximum quantity in base currency that you can buy
    /// The cross margin order under Single-currency margin mode, number of coins is based on base currency.
    /// FUTURES/SWAP/OPTIONS: The maximum number of contracts that you can buy
    /// </summary>
    [JsonProperty("maxBuy")]
    public decimal MaximumBuy { get; set; }

    /// <summary>
    /// SPOT/MARGIN: The maximum quantity in quote currency that you can sell
    /// The cross margin order under Single-currency margin mode, number of coins is based on base currency.
    /// FUTURES/SWAP/OPTIONS: The maximum number of contracts that you can sell
    /// </summary>
    [JsonProperty("maxSell")]
    public decimal MaximumSell { get; set; }
}
