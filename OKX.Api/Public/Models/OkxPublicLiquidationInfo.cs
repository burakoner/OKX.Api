namespace OKX.Api.Public;

/// <summary>
/// OKX Liquidation Info
/// </summary>
public class OkxPublicLiquidationInfo
{
    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }

    /// <summary>
    /// Instrument Type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Total Loss
    /// </summary>
    [JsonProperty("totalLoss")]
    public decimal? TotalLoss { get; set; }

    /// <summary>
    /// Underlying
    /// </summary>
    [JsonProperty("uly")]
    public string Underlying { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxPublicLiquidationInfoDetail> Details { get; set; } = [];
}

/// <summary>
/// OKX Public Liquidation Info Detail
/// </summary>
public class OkxPublicLiquidationInfoDetail
{
    /// <summary>
    /// Order Side
    /// </summary>
    [JsonProperty("side"), JsonConverter(typeof(OkxTradeOrderSideConverter))]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position Side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Bankruptcy Price
    /// </summary>
    [JsonProperty("bkPx")]
    public decimal? BankruptcyPrice { get; set; }

    /// <summary>
    /// Number of Liquidations
    /// </summary>
    [JsonProperty("sz")]
    public decimal? NumberOfLiquidations { get; set; }

    /// <summary>
    /// Number of Losses
    /// </summary>
    [JsonProperty("bkLoss")]
    public decimal? NumberOfLosses { get; set; }

    /// <summary>
    /// Liquidation Price
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}