﻿using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Models;

/// <summary>
/// OKX One Click Repay Currency List
/// </summary>
public class OkxTradeOneClickRepayCurrencyList
{
    /// <summary>
    /// Debt currency data list
    /// </summary>
    [JsonProperty("debtData")]
    public List<OkxTradeOneClickDeptData> Debt { get; set; } = [];
    
    /// <summary>
    /// Debt type
    /// </summary>
    [JsonProperty("debtType"), JsonConverter(typeof(OkxTradeDebtTypeConverter))]
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
public class OkxTradeOneClickDeptData
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
public class OkxTradeOneClickRepayData
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