﻿using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Funding Bill
/// </summary>
public class OkxFundingBill
{
    /// <summary>
    /// Bill ID
    /// </summary>
    [JsonProperty("billId")]
    public long? BillId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Client Order ID
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Balance
    /// </summary>
    [JsonProperty("bal")]
    public decimal? Balance { get; set; }

    /// <summary>
    /// Balance Change
    /// </summary>
    [JsonProperty("balChg")]
    public decimal? BalanceChange { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxFundingBillTypeConverter))]
    public OkxFundingBillType Type { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}