using OKX.Api.SubAccount.Converters;
using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Models;

/// <summary>
/// OKX Sub Account Bill
/// </summary>
public class OkxSubAccountBill
{
    /// <summary>
    /// Bill Id
    /// </summary>
    [JsonProperty("billId")]
    public long BillId { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxSubAccountTransferTypeConverter))]
    public OkxSubAccountTransferType Type { get; set; }

    /// <summary>
    /// Sub Account Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}

/// <summary>
/// OKX Managed Sub Account Bill
/// </summary>
public class OkxSubAccountManagedBill : OkxSubAccountBill
{
    /// <summary>
    /// Sub Account Id
    /// </summary>
    [JsonProperty("subUid")]
    public long SubAccountId { get; set; }
}