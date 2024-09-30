namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Receiver
/// </summary>
public record OkxFundingWithdrawalReceiver
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("walletType")]
    public string WalletType { get; set; } = string.Empty;

    /// <summary>
    /// Chain
    /// </summary>
    [JsonProperty("exchId")]
    public string ExchangeId { get; set; } = string.Empty;

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("rcvrFirstName")]
    public string ReceiverFirstName { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("rcvrLastName")]
    public string ReceiverLastName { get; set; } = string.Empty;

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("rcvrCountry")]
    public string ReceiverCountry { get; set; } = string.Empty;

    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("rcvrCountrySubDivision")]
    public string ReceiverCountrySubDivision { get; set; } = string.Empty;

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("rcvrTownName")]
    public string ReceiverTownName { get; set; } = string.Empty;

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("rcvrTownName")]
    public string ReceiverStreetName { get; set; } = string.Empty;
}