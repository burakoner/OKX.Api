using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Models.FundingAccount;

public class OkxTransferResponse
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("transId")]
    public long? TransferId { get; set; }

    [JsonProperty("clientId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("from"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount? From { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount? To { get; set; }
}