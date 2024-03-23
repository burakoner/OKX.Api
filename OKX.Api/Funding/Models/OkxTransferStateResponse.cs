using OKX.Api.Funding.Converters;
using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Models;

public class OkxTransferStateResponse
{
    [JsonProperty("transId")]
    public long TransferId { get; set; }
    
    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }
    
    [JsonProperty("type"), JsonConverter(typeof(OkxTransferTypeConverter))]
    public OkxTransferType Type { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount RemittingAccount { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount BeneficiaryAccount { get; set; }

    [JsonProperty("subAcct")]
    public string SubAccount { get; set; }
    
    [JsonProperty("state"), JsonConverter(typeof(OkxTransferStateConverter))]
    public OkxTransferState State { get; set; }
}