namespace OKX.Api.Models.Account;

public class OkxClosingPosition
{
    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    [JsonProperty("type"), JsonConverter(typeof(ClosingPositionTypeConverter))]
    public OkxClosingPositionType Type { get; set; }

    [JsonProperty("cTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreateTime { get; set; }

    [JsonProperty("uTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("openAvgPx")]
    public decimal? OpenAveragePrice { get; set; }

    [JsonProperty("closeAvgPx")]
    public decimal? CloseAveragePrice { get; set; }

    [JsonProperty("posId")]
    public long? PositionId { get; set; }

    [JsonProperty("openMaxPos")]
    public decimal? OpenMaxPos { get; set; }

    [JsonProperty("closeTotalPos")]
    public decimal? CloseTotalPos { get; set; }

    [JsonProperty("pnl")]
    public decimal? ProfitLoss { get; set; }

    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    [JsonProperty("direction"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide Direction { get; set; }

    [JsonProperty("triggerPx")]
    public decimal? TriggerMarkPrice { get; set; }

    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }
}