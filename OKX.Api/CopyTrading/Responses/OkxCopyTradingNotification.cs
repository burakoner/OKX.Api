namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxCopyTradingNotification
/// </summary>
public record OkxCopyTradingNotification
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }
    
    /// <summary>
    /// Information type
    /// 1: lead trading failed due to touch max position limitation
    /// 2: lead trading failed due to touch the maximum daily number of lead trading
    /// 3: lead trading failed due to your USDT equity less than the minimum USDT equity of lead trading
    /// </summary>
    [JsonProperty("infoType")]
    public OkxCopyTradingInformationType InformationType { get; set; }

    /// <summary>
    /// Lead position ID
    /// </summary>
    [JsonProperty("subPosId")]
    public long? SubPositionId { get; set; }

    /// <summary>
    /// User unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; } = string.Empty;

    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Side buy sell
    /// </summary>
    [JsonProperty("side")]
    public OkxTradeOrderSide OrderSide { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Maximum daily number of lead trading.
    /// </summary>
    [JsonProperty("maxLeadTraderNum")]
    public int MaximumDailyNumberOfLeadTrading { get; set; }

    /// <summary>
    /// Minimum USDT equity of lead trading.
    /// </summary>
    [JsonProperty("minLeadEq")]
    public decimal? MinimumUsdtEquityOfLeadTrading { get; set; }
}