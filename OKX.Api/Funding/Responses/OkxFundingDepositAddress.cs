namespace OKX.Api.Funding;

/// <summary>
/// Deposit Address
/// </summary>
public record OkxFundingDepositAddress
{
    /// <summary>
    /// Deposit address
    /// </summary>
    [JsonProperty("addr")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Deposit tag (This will not be returned if the currency does not require a tag for deposit)
    /// </summary>
    [JsonProperty("tag")]
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Deposit memo (This will not be returned if the currency does not require a memo for deposit)
    /// </summary>
    [JsonProperty("memo")]
    public string Memo { get; set; } = string.Empty;

    /// <summary>
    /// Deposit payment ID (This will not be returned if the currency does not require a payment_id for deposit)
    /// </summary>
    [JsonProperty("pmtId")]
    public string DepositPaymentId { get; set; } = string.Empty;

    //[JsonProperty("addrEx")]
    //public object DepositAddressAttachment { get; set; }

    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; } = string.Empty;

    /// <summary>
    /// The beneficiary account
    /// </summary>
    [JsonProperty("to")]
    public OkxAccount BeneficiaryAccount { get; set; }
    
    /// <summary>
    /// Verified name (for recipient)
    /// </summary>
    [JsonProperty("verifiedName")]
    public string VerifiedName { get; set; } = string.Empty;

    /// <summary>
    /// Return true if the current deposit address is selected by the website page
    /// </summary>
    [JsonProperty("selected")]
    public bool Selected { get; set; }

    /// <summary>
    /// Last 6 digits of contract address
    /// </summary>
    [JsonProperty("ctAddr")]
    public string ContractAddress { get; set; } = string.Empty;
}
