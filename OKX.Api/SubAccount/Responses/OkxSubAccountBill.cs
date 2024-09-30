namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Bill
/// </summary>
public record OkxSubAccountBill
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
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type")]
    public OkxSubAccountTransferType Type { get; set; }

    /// <summary>
    /// Sub Account Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
}

/// <summary>
/// OKX Managed Sub Account Bill
/// </summary>
public record OkxSubAccountManagedBill : OkxSubAccountBill
{
    /// <summary>
    /// Sub Account Id
    /// </summary>
    [JsonProperty("subUid")]
    public long SubAccountId { get; set; }
}