using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Transfer Response
/// </summary>
public class OkxTransferResponse
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Transfer Id
    /// </summary>
    [JsonProperty("transId")]
    public long TransferId { get; set; }

    /// <summary>
    /// Client Order Id
    /// </summary>
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Amount
    /// </summary>
    [JsonProperty("amt")]
    public decimal Amount { get; set; }

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
}