namespace OKX.Api.Trade;

/// <summary>
/// OKX One Click Repay Currency List
/// </summary>
public record OkxTradeOneClickRepayCurrencyList
{
    /// <summary>
    /// Debt currency data list
    /// </summary>
    [JsonProperty("debtData")]
    public List<OkxTradeOneClickDeptData> Debt { get; set; } = [];
    
    /// <summary>
    /// Debt type
    /// </summary>
    [JsonProperty("debtType")]
    public OkxTradeDebtType Type { get; set; }

    /// <summary>
    /// Repay currency data list
    /// </summary>
    [JsonProperty("repayData")]
    public List<OkxTradeOneClickRepayData> Repay { get; set; } = [];
}

/// <summary>
/// OKX One Click Debt Data
/// </summary>
public record OkxTradeOneClickDeptData
{
    /// <summary>
    /// Debt currency type
    /// </summary>
    [JsonProperty("debtCcy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Debt currency amount. Including principal and interest
    /// </summary>
    [JsonProperty("debtAmt")]
    public decimal DebtAmount { get; set; }
}

/// <summary>
/// OKX One Click Repay Data
/// </summary>
public record OkxTradeOneClickRepayData
{
    /// <summary>
    /// Repay currency type
    /// </summary>
    [JsonProperty("repayCcy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Repay currency's available balance amount
    /// </summary>
    [JsonProperty("repayAmt")]
    public decimal RepayAmount { get; set; }
}