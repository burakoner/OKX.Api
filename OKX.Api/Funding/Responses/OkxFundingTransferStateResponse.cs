namespace OKX.Api.Funding;

/// <summary>
/// OKX Transfer State Response
/// </summary>
public class OkxFundingTransferStateResponse
{
    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long TransferId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; } = string.Empty;

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
    [JsonProperty("type"), JsonConverter(typeof(OkxFundingTransferTypeConverter))]
    public OkxFundingTransferType Type { get; set; }

    /// <summary>
    /// Remitting Account
    /// </summary>
    [JsonProperty("from"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount RemittingAccount { get; set; }

    /// <summary>
    /// Beneficiary Account
    /// </summary>
    [JsonProperty("to"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount BeneficiaryAccount { get; set; }

    /// <summary>
    /// Sub Account
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccount { get; set; } = string.Empty;

    /// <summary>
    /// State
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(OkxFundingTransferStateConverter))]
    public OkxFundingTransferState State { get; set; }
}