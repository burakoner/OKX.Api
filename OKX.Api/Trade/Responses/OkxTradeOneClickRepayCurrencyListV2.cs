namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Currency List V2
/// </summary>
public record OkxTradeOneClickRepayCurrencyListV2
{
    /// <summary>
    /// Debt currency data list
    /// </summary>
    [JsonProperty("debtData")]
    public List<OkxTradeOneClickDeptData> Debt { get; set; } = [];
    
    /// <summary>
    /// Repay currency data list
    /// </summary>
    [JsonProperty("repayData")]
    public List<OkxTradeOneClickRepayData> Repay { get; set; } = [];
}