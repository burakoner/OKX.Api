namespace OKX.Api.Public.Models;

public class OkxLiquidationInfo
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("totalLoss")]
    public decimal? TotalLoss { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("details")]
    public List<OkxPublicLiquidationInfoDetail> Details { get; set; }
}

public class OkxPublicLiquidationInfoDetail
{
    [JsonProperty("side"), JsonConverter(typeof(OkxOrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(OkxPositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    [JsonProperty("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    [JsonProperty("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}