namespace OKX.Api.Funding;

/// <summary>
/// OKX Withdrawal Receiver
/// </summary>
public class OkxFundingWithdrawalReceiver
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("walletType")]
    public string WalletType { get; set; } = "";

    /// <summary>
    /// Chain
    /// </summary>
    [JsonProperty("exchId")]
    public string ExchangeId { get; set; } = "";

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("rcvrFirstName")]
    public string ReceiverFirstName { get; set; } = "";

    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("rcvrLastName")]
    public string ReceiverLastName { get; set; } = "";

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("rcvrCountry")]
    public string ReceiverCountry { get; set; } = "";

    /// <summary>
    /// Withdrawal Id
    /// </summary>
    [JsonProperty("rcvrCountrySubDivision")]
    public string ReceiverCountrySubDivision { get; set; } = "";

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("rcvrTownName")]
    public string ReceiverTownName { get; set; } = "";

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("rcvrTownName")]
    public string ReceiverStreetName { get; set; } = "";
}