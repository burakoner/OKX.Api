namespace OKX.Api.Models.MarketData;

/// <summary>
/// OkxOptionTrade
/// </summary>
public class OkxOptionTradeByInstrumentFamily
{
    /// <summary>
    /// 24h trading volume, with a unit of contract.
    /// </summary>
    [JsonProperty("vol24h")]
    public decimal Volume24H { get; set; }

    /// <summary>
    /// The list trade data
    /// </summary>
    [JsonProperty("tradeInfo")]
    public IEnumerable<OkxOptionTradeInfo> TradeInfo { get; set; }

    /// <summary>
    /// Option type, C: Call P: Put
    /// </summary>
    [JsonProperty("optType"), JsonConverter(typeof(OptionTypeConverter))]
    public OkxOptionType OptionType { get; set; }
}