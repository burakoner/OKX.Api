namespace OKX.Api.Models.TradingAccount;

public class OkxFeeRate
{
    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("taker")]
    public decimal? Taker { get; set; }

    [JsonProperty("maker")]
    public decimal? Maker { get; set; }
    
    [JsonProperty("takerU")]
    public decimal? TakerUsdtPairsAndContracts { get; set; }

    [JsonProperty("makerU")]
    public decimal? MakerUsdtPairsAndContracts { get; set; }

    [JsonProperty("delivery")]
    public decimal? Delivery { get; set; }

    [JsonProperty("exercise")]
    public decimal? Exercise { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("takerUSDC")]
    public decimal? TakerUsdcPairsAndContracts { get; set; }

    [JsonProperty("makerUSDC")]
    public decimal? MakerUsdcPairsAndContracts { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
