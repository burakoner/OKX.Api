namespace OKX.Api.Models.PublicData;

public class OkxLiquidationInfo
{
    [JsonProperty("instId")]
    public string Instrument { get; set; }

    [JsonProperty("instType"), JsonConverter(typeof(InstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    [JsonProperty("totalLoss")]
    public decimal? TotalLoss { get; set; }

    [JsonProperty("uly")]
    public string Underlying { get; set; }

    [JsonProperty("details")]
    public IEnumerable<OkxPublicLiquidationInfoDetail> Details { get; set; }
}

public class OkxPublicLiquidationInfoDetail
{
    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public OkxOrderSide OrderSide { get; set; }

    [JsonProperty("posSide"), JsonConverter(typeof(PositionSideConverter))]
    public OkxPositionSide PositionSide { get; set; }

    [JsonProperty("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    [JsonProperty("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    [JsonProperty("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}