namespace OKX.Api.Financial.FlexibleSimpleEarn.Models;

/// <summary>
/// OKX Flexible Simple Earn Savings Borrow Summary
/// </summary>
public class OkxFlexibleSimpleEarnSavingsBorrowSummary
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    /// <summary>
    /// 24H average borrowing amount
    /// </summary>
    [JsonProperty("avgAmt")]
    public decimal AverageAmount { get; set; }

    /// <summary>
    /// 24H average borrowing amount in USD value
    /// </summary>
    [JsonProperty("avgAmtUsd")]
    public decimal AverageAmountUsd { get; set; }

    /// <summary>
    /// 24H average lending rate
    /// </summary>
    [JsonProperty("avgRate")]
    public decimal AverageRate { get; set; }

    /// <summary>
    /// Last annual interest rate
    /// </summary>
    [JsonProperty("preRate")]
    public decimal PreviousRate { get; set; }

    /// <summary>
    /// Next estimate annual interest rate
    /// </summary>
    [JsonProperty("estRate")]
    public decimal EstimatedRate { get; set; }
}
