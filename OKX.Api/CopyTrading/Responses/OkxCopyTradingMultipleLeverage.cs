namespace OKX.Api.CopyTrading;

/// <summary>
/// OkxMultipleLeverage
/// </summary>
public record OkxCopyTradingMultipleLeverage
{
    /// <summary>
    /// Instrument ID
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode")]
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
public record OkxCopyTradingLeverageData()
{
    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public int Leverage { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }
}
