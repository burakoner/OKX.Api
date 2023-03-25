namespace OKX.Api.Models.Account;

public class OkxPositionRisk
{
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("balData")]
    public IEnumerable<OkxAccountPositionRiskBalanceData> BalanceData { get; set; }

    [JsonProperty("posData")]
    public IEnumerable<OkxAccountPositionRiskPositionData> PositionData { get; set; }
}

public class OkxAccountPositionRiskBalanceData
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }

    [JsonProperty("eq")]
    public decimal? Equity { get; set; }
}

public class OkxAccountPositionRiskPositionData
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("mgnMode"), JsonConverter(typeof(MarginModeConverter))]
    public OkxMarginMode MarginMode { get; set; }

    [JsonProperty("notionalCcy")]
    public decimal? NotionalCcy { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; }

    [JsonProperty("posId")]
    public long PositionId { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }
}
