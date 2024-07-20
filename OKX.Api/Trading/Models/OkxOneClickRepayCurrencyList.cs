using OKX.Api.Trading.Converters;
using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Models;

/// <summary>
/// OKX One Click Repay Currency List
/// </summary>
public class OkxOneClickRepayCurrencyList
{
    /// <summary>
    /// Debt currency data list
    /// </summary>
    [JsonProperty("debtData")]
    public List<OkxOneClickDeptData> Debt { get; set; } = [];
    
    /// <summary>
    /// Debt type
    /// </summary>
    [JsonProperty("debtType"), JsonConverter(typeof(OkxDebtTypeConverter))]
    public OkxDebtType Type { get; set; }

    /// <summary>
    /// Repay currency data list
    /// </summary>
    [JsonProperty("repayData")]
    public List<OkxOneClickRepayData> Repay { get; set; } = [];
}

/// <summary>
/// OKX One Click Debt Data
/// </summary>
public class OkxOneClickDeptData
{
    /// <summary>
    /// Debt currency type
    /// </summary>
    [JsonProperty("debtCcy")]
    public string Currency { get; set; }

    /// <summary>
    /// Debt currency amount. Including principal and interest
    /// </summary>
    [JsonProperty("debtAmt")]
    public decimal DebtAmount { get; set; }
}

/// <summary>
/// OKX One Click Repay Data
/// </summary>
public class OkxOneClickRepayData
{
    /// <summary>
    /// Repay currency type
    /// </summary>
    [JsonProperty("repayCcy")]
    public string Currency { get; set; }

    /// <summary>
    /// Repay currency's available balance amount
    /// </summary>
    [JsonProperty("repayAmt")]
    public decimal RepayAmount { get; set; }
}