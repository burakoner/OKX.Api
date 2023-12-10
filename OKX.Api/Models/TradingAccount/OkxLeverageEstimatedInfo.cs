namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxLeverageEstimatedInfo
/// </summary>
public class OkxLeverageEstimatedInfo
{
    /// <summary>
    /// The estimated margin(in quote currency) can be transferred out under the corresponding leverage
    /// For cross, it is the maximum quantity that can be transferred from the trading account.
    /// For isolated, it is the maximum quantity that can be transferred from the isolated position
    /// </summary>
    [JsonProperty("estAvailQuoteTrans")]
    public decimal? EstimatedAvailableQouteMargin { get; set; }

    /// <summary>
    /// The estimated margin(in base currency) can be transferred out under the corresponding leverage
    /// For cross, it is the maximum quantity that can be transferred from the trading account.
    /// For isolated, it is the maximum quantity that can be transferred from the isolated position
    /// </summary>
    [JsonProperty("estAvailTrans")]
    public decimal? EstimatedAvailableBaseMargin { get; set; }

    /// <summary>
    /// The estimated liquidation price under the corresponding leverage. Only return when there is a position.
    /// </summary>
    [JsonProperty("estLiqPx")]
    public decimal? EstimatedLiquidationPrice { get; set; }

    /// <summary>
    /// The estimated margin needed by position under the corresponding leverage.
    /// For the MARGIN position, it is margin in base currency
    /// </summary>
    [JsonProperty("estMgn")]
    public decimal? EstimatedBaseMargin { get; set; }

    /// <summary>
    /// The estimated margin (in quote currency) needed by position under the corresponding leverage
    /// </summary>
    [JsonProperty("estQuoteMgn")]
    public decimal? EstimatedQuoteMargin { get; set; }

    /// <summary>
    /// For MARGIN, it is the estimated maximum loan in base currency under the corresponding leverage
    /// For SWAP and FUTURES, it is the estimated maximum quantity of contracts that can be opened under the corresponding leverage
    /// </summary>
    [JsonProperty("estMaxAmt")]
    public decimal? EstimatedMaximumBaseLoan { get; set; }

    /// <summary>
    /// The MARGIN estimated maximum loan in quote currency under the corresponding leverage.
    /// </summary>
    [JsonProperty("estQuoteMaxAmt")]
    public decimal? EstimatedMaximumQuoteLoan { get; set; }

    /// <summary>
    /// Whether there is pending orders
    /// </summary>
    [JsonProperty("existOrd")]
    public bool PendingOrders { get; set; }

    /// <summary>
    /// Maximum leverage
    /// </summary>
    [JsonProperty("maxLever")]
    public decimal? MaximumLeverage { get; set; }

    /// <summary>
    /// Minimum leverage
    /// </summary>
    [JsonProperty("minLever")]
    public decimal? MinimumLeverage { get; set; }
}
