namespace OKX.Api.Trade;

/// <summary>
/// OKX Order
/// </summary>
public class OkxTradeOrderCheck
{
    /// <summary>
    /// Current adjusted / Effective equity in USD
    /// </summary>
    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    /// <summary>
    /// After placing order, changed quantity of adjusted / Effective equity in USD
    /// </summary>
    [JsonProperty("adjEqChg")]
    public decimal? AdjustedEquityChange { get; set; }

    /// <summary>
    /// Current initial margin requirement in USD
    /// </summary>
    [JsonProperty("imr")]
    public decimal? IMR { get; set; }
    
    /// <summary>
    /// After placing order, changed quantity of initial margin requirement in USD
    /// </summary>
    [JsonProperty("imrChg")]
    public decimal? IMRChange { get; set; }

    /// <summary>
    /// Current Maintenance margin requirement in USD
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MMR { get; set; }

    /// <summary>
    /// After placing order, changed quantity of maintenance margin requirement in USD
    /// </summary>
    [JsonProperty("mmrChg")]
    public decimal? MMRChange { get; set; }

    /// <summary>
    /// Current margin ratio in USD
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// After placing order, changed quantity of margin ratio in USD
    /// </summary>
    [JsonProperty("mgnRatioChg")]
    public decimal? MarginRatioChange { get; set; }

    /// <summary>
    /// Current available balance in margin coin currency, only applicable to turn auto borrow off
    /// </summary>
    [JsonProperty("availBal")]
    public decimal? AvailableBalance { get; set; }

    /// <summary>
    /// After placing order, changed quantity of available balance after placing order, only applicable to turn auto borrow off
    /// </summary>
    [JsonProperty("availBalChg")]
    public decimal? AvailableBalanceChange { get; set; }
    
    /// <summary>
    /// Current estimated liquidation price
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// After placing order, the distance between estimated liquidation price and mark price
    /// </summary>
    [JsonProperty("liqPxDiff")]
    public decimal? LiquidationPriceDiff { get; set; }

    /// <summary>
    /// After placing order, the distance rate between estimated liquidation price and mark price
    /// </summary>
    [JsonProperty("liqPxDiffRatio")]
    public decimal? LiquidationPriceDiffRatio { get; set; }

    /// <summary>
    /// Current positive asset, only applicable to margin isolated position
    /// </summary>
    [JsonProperty("posBal")]
    public decimal? PositionBalance { get; set; }

    /// <summary>
    /// After placing order, positive asset of margin isolated, only applicable to margin isolated position
    /// </summary>
    [JsonProperty("posBalChg")]
    public decimal? PositionBalanceChange { get; set; }

    /// <summary>
    /// Current liabilities of currency
    /// For cross, it is cross liabilities
    /// For isolated position, it is isolated liabilities
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liability  { get; set; }

    /// <summary>
    /// After placing order, changed quantity of liabilities
    /// For cross, it is cross liabilities
    /// For isolated position, it is isolated liabilities
    /// </summary>
    [JsonProperty("liabChg")]
    public decimal? LiabilityChange  { get; set; }

    /// <summary>
    /// After placing order, the unit of changed liabilities quantity
    /// only applicable cross and in auto borrow
    /// </summary>
    [JsonProperty("liabChgCcy")]
    public string LiabilityChangeCurrency  { get; set; } = "";

    /// <summary>
    /// Unit type of positive asset, only applicable to margin isolated position
    /// 1: it is both base currency before and after placing order
    /// 2: before plaing order, it is base currency. after placing order, it is quota currency.
    /// 3: before plaing order, it is quota currency. after placing order, it is base currency
    /// 4: it is both quota currency before and after placing order
    /// </summary>
    [JsonProperty("type")]
    public string Type  { get; set; } = "";
}
