using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;

namespace OKX.Api.Funding.Models;

public class OkxTransferResponse
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("transId")]
    public long TransferId { get; set; }

    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount RemittingAccount { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount BeneficiaryAccount { get; set; }
}