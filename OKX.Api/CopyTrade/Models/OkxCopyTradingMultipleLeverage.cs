namespace OKX.Api.CopyTrade;

/// <summary>
/// OkxMultipleLeverage
/// </summary>
public class OkxCopyTradingMultipleLeverage
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = "";

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode"), JsonConverter(typeof(OkxAccountMarginModeConverter))]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Lead trader leverages
    /// </summary>
    [JsonProperty("leadTraderLevers")]
    public List<OkxCopyTradingLeverageData> LeadTraderLeverages { get; set; } = [];

    /// <summary>
    /// My leverages
    /// </summary>
    [JsonProperty("myLevers")]
    public List<OkxCopyTradingLeverageData> MyLeverages { get; set; } = [];
}

/// <summary>
/// LeverageData
/// </summary>
public class OkxCopyTradingLeverageData()
{
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide"), JsonConverter(typeof(OkxTradePositionSideConverter))]
    public OkxTradePositionSide PositionSide { get; set; }
}
